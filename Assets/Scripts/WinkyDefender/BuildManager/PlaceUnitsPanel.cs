using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaceUnitsPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public Text	unitLeftText;
	public Color defaultColor;
	public Color selectionColor;
	private GameObject _turret;
	private int _unitLeft;
	private BuildManager _buildManager;
	private TileManager	_tile;
	private PlaceUnitsPanel _previousUnitSelected;

	// Use this for initialization
	void Start ()
	{
		_buildManager = BuildManager.instance;
		unitLeftText.text = _unitLeft.ToString ();
		defaultColor = Color.white;
		selectionColor = Color.yellow;
		SetUnitLeft (GameObject.Find ("MapEditor").GetComponent<MapEditor> ().GetNbUnitsOfOneUnit (this.name));
	}

	public int GetUnitLeft()
	{
		return (_unitLeft);
	}

	public void SetUnitLeft(int unitLeft)
	{
		_unitLeft = unitLeft;
		unitLeftText.text = _unitLeft.ToString ();
		if (_unitLeft <= 0)
			RemoveColor (this);
	}

	/*
	 ** Set default color to the unit icon in the unit panel
	 */
	public void RemoveColor(PlaceUnitsPanel unitSelected)
	{
		unitSelected.GetComponent<Image> ().color = defaultColor;
	}

	/*
	 ** Set selection color to the unit icon in the unit panel
	 */
	private void _AddColor(PlaceUnitsPanel unitSelected)
	{
		unitSelected.GetComponent<Image>().color = selectionColor;
	}

	/*
	 ** Send the correct unit prefab and the unit type to the buildManager
	 */
	public void SelectUnitToBuild()
	{
		if (_unitLeft > 0)
		{
			_buildManager.SetTurretToBuild (GameObject.Find ("Database").GetComponent<Database> ().GetPrefab ("Unit", this.name));
			_buildManager.SetUnitType (this);
		}
	}

	/*
	 ** Send null value as unit prefab and unit type to the buildManager
	 */
	public void DeselectUnitToBuild()
	{
		_buildManager.SetTurretToBuild (null);
		_buildManager.SetUnitType (null);
	}

	/*
	 ** If an unit is already selected and a new one is selected, remove highlight and deselect the old one, then highlight and select the new one
	 ** If an unit is already selected and the same is selected againm remove highlight and deselect it
	 ** If no unit is selected, highlight it and selecte it
	 */
	public void OnPointerClick(PointerEventData eventData)
	{
		_previousUnitSelected = _buildManager.GetUnitType ();
		if (_previousUnitSelected)
		{
			if (this == _previousUnitSelected)
			{
				RemoveColor (_previousUnitSelected);
				DeselectUnitToBuild ();
			}
			else
			{
				RemoveColor (_previousUnitSelected);
				_AddColor (this);
				SelectUnitToBuild ();
			}
		}
		else
		{
			_AddColor (this);
			SelectUnitToBuild();
		}
	}

	/*
	 ** On begining drag, highlight the dragged unit icon in unit panel and create the unit with the correspongding prefab
	 */
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (_unitLeft > 0)
		{
			GameObject.Find ("Main Camera Top View").GetComponent<MoveCameraInGame> ().SetMoveAllowed (false);
			_previousUnitSelected = _buildManager.GetUnitType ();
			if (_previousUnitSelected && this != _previousUnitSelected)
			{
					RemoveColor (_previousUnitSelected);
					_AddColor (this);
			}
			else if (!_previousUnitSelected)
				_AddColor(this);
			_turret = _buildManager.CreateTurret (GameObject.Find("Database").GetComponent<Database>().GetPrefab("Unit", this.name));

		}
	}

	/*
	 ** On dragging, round the unit's position to force position to be at center of the tile the unit hovers
	 */
	public void OnDrag(PointerEventData eventData)
	{
		if (_unitLeft > 0)
		{
			RaycastHit[] hits; 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			hits = Physics.RaycastAll (ray, 50f);
			if (hits != null && hits.Length > 0)
			{
				_turret.transform.position = hits [0].point;
				_turret.transform.position = new Vector3 (Mathf.Round (_turret.transform.position.x),
					_turret.transform.position.y,
					Mathf.Round (_turret.transform.position.z));
				_turret.SetActive (true);
				
			}
		}
	}

	/*
	 ** Return the index of hits when UnitSpawn is hit.
	 ** If no UnitSpawn was hit, return -1
	 */
	private int _GetTerrainColliderQuadIndex(RaycastHit[] hits)
	{
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits [i].collider.gameObject.tag.Equals ("UnitSpawn"))
				return i;
		}
		return -1;
	}

	/*
	 ** If dragging action doesn't end on UnitSpawn, destroy the dragged unit
	 ** If dragging action ends on UnitSpawn and a unit is already on it, destroy the dragged unit
	 ** If dragging action ends on UnitSpawn and the UnitSpawn doesn't have a unit on it, then put the unit on the title
	 */
	public void OnEndDrag(PointerEventData eventData)
	{
		if (_unitLeft > 0)
		{
			GameObject.Find ("Main Camera Top View").GetComponent<MoveCameraInGame> ().SetMoveAllowed (true);
			RaycastHit[] hits;
			int index;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RemoveColor (this);
			hits = Physics.RaycastAll (ray, 50f);
			if (hits != null && hits.Length > 0)
			{
				index = _GetTerrainColliderQuadIndex (hits);
				if (index != -1)
				{
					_tile = hits [index].collider.gameObject.GetComponent<TileManager> ();
					if (!_tile.TurretOnTile ())
					{
						_buildManager.PutTurretOnTile (_turret, _tile, this);
						_previousUnitSelected = _buildManager.GetUnitType ();
						if (_previousUnitSelected)
						{
							RemoveColor (_previousUnitSelected);
							DeselectUnitToBuild ();
						}
					}
					else
					{
						Destroy (_turret);
					}
				}
				else
					Destroy (_turret);
			}
			else
				Destroy (_turret);
		}
	}
}
