using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
#endif
public class BuildUnits : MonoBehaviour {

	private Database _database;
//	private GameObject _unitsParent;
	private UnitData[] _units;
	private GameObject _unitToCreate;

	#if UNITY_EDITOR
	void Update ()
	{
		if(EditorApplication.isPlaying)
			return;
		_UpdateUnits();
	}
	#else
	void Start () {
	_UpdateUnits();
	}
	#endif

	private Vector3 _GetOrientation(string orientation)
	{
		if (orientation == "top")
			return (new Vector3 (0, 0, 0));
		else if (orientation == "right")
			return (new Vector3 (0, 90, 0));
		else if (orientation == "down")
			return (new Vector3 (0, 180, 0));
		else if (orientation == "left")
			return (new Vector3 (0, 270, 0));
		else
			return (new Vector3 (0, 0, 0));
	}

	private Vector3 _GetPosition (Vector2 position)
	{
		return (new Vector3 (position.x, 0, position.y));
	}

	private void _UpdateUnits()
	{
		_units =  GameObject.Find ("MapEditor").GetComponent<MapEditor> ().units;
		_database = GameObject.Find("Database").GetComponent<Database>();
//		if (_ResetNeeded ())
			_ResetUnits ();
	}

	private bool _ResetNeeded()
	{
		if (this.transform.childCount != _units.Length)
			return (true);
		int i = 0;
		foreach (UnitData unit in _units)
		{
			Transform child = this.transform.GetChild (i);
			if (unit.unitType.ToString () != child.name)
				return (true);
			else if (_GetPosition(unit.position) != child.position)
				child.position = _GetPosition (unit.position);
			else if (_GetOrientation(unit.orientation.ToString()) != child.eulerAngles)
				child.eulerAngles = _GetOrientation (unit.orientation.ToString());
			i++;
		}
		return (false);
	}

	private void _ResetUnits()
	{
		_DestroyUnits ();
		_CreateUnits ();
	}

	private void _DestroyUnits()
	{
		int children = this.transform.childCount;
		for (int i = 0; i < children; i++)
			DestroyImmediate(this.transform.GetChild (0).gameObject);
	}

	private void _CreateUnits()
	{
		foreach (UnitData unit in _units)
		{
			_unitToCreate = Instantiate (_database.GetPrefab("Unit", unit.unitType.ToString()));
			_unitToCreate.transform.SetParent(GameObject.Find("Units").transform);
			_unitToCreate.transform.position = _GetPosition(unit.position);
			_unitToCreate.transform.eulerAngles = _GetOrientation (unit.orientation.ToString());
			_unitToCreate.name = unit.unitType.ToString();
		}
	}
}
