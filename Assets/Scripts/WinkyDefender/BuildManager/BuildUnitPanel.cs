using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
#endif

public class BuildUnitPanel : MonoBehaviour {

	private Database _database;
	private GameObject _unitPanelToCreate;
	private UnitDataPanel[] _units;

	#if UNITY_EDITOR
	void Update ()
	{
		if(EditorApplication.isPlaying)
			return;
		_UpdateUnitPanel();
	}
	#else
	void Start () {
		_UpdateUnitPanel();
	}
#endif

	/*
	 ** Update Unit Panel if any change happened on it.
	 */
	private void _UpdateUnitPanel()
	{
		_database = GameObject.Find("Database").GetComponent<Database>();
		_units =  GameObject.Find ("MapEditor").GetComponent<MapEditor> ().unitsInPanel;
		if (_ResetNeeded ())
			_ResetUnitPanel ();
	}

	/*
	 ** Return true if the number or the type of elements on Unit Panel have changed.
	 ** Return false if nothing changed or if only the number of one type of unit changed (but update it before return).
	 */
	private bool _ResetNeeded()
	{
		if (this.transform.childCount != _units.Length)
			return (true);
		int i = 0;
		foreach (UnitDataPanel unit in _units)
		{
			Transform unitPanel = this.transform.GetChild (i);
			if (unit.unitType.ToString () != unitPanel.name)
				return (true);
			else if (unit.nbUnits != unitPanel.GetComponent<PlaceUnitsPanel>().GetUnitLeft ())
				unitPanel.GetComponent<PlaceUnitsPanel>().SetUnitLeft (unit.nbUnits);
			i++;
		}
		return (false);
	}

	/*
	 ** Reset Unit Panel by estroying and recreate it.
	 */
	private void _ResetUnitPanel()
	{
		_DestroyUnitPanel ();
		_CreateUnitPanel ();
	}

	/*
	 ** Destroy all elements of Unit Panel.
	 */
	private void _DestroyUnitPanel()
	{
		int children = this.transform.childCount;
		for (int i = 0; i < children; i++)
			DestroyImmediate(this.transform.GetChild (0).gameObject);
	}

	/*
	 ** Create as much elements in Unit Panel as the mapEditor units[] size, with the corresponding prefab.
	 */
	private void _CreateUnitPanel()
	{
		foreach (UnitDataPanel unit in _units)
		{
			_unitPanelToCreate = Instantiate (_database.GetPrefab("UnitPanel", unit.unitType.ToString()));
			_unitPanelToCreate.transform.SetParent(this.transform);
			_unitPanelToCreate.name = unit.unitType.ToString();
			_unitPanelToCreate.GetComponent<PlaceUnitsPanel> ().SetUnitLeft (unit.nbUnits);
		}
	}
}