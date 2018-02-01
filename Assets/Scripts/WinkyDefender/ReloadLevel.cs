using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour {

	private Transform 	_units;
	private Transform	_child;
	private Unit		_unitScript;

	public void Start () {
		_units = this.gameObject.transform;
	}

	public void SaveAllUnits()
	{
		int children = _units.childCount;
		for (int i = 0; i < children; i++)
		{
			_SetChildScript (i);
			_unitScript.SetInitialPosition(_child.position);
			_unitScript.SetInitialRotation(_child.eulerAngles);
		}
	}

	public void ResetAllUnits()
	{
		int children = _units.childCount;
		for (int i = 0; i < children; i++)
		{
			_SetChildScript (i);
			_child.position = _unitScript.GetInitialPosition ();
			_child.eulerAngles = _unitScript.GetInitialRotation ();
		}
	}

	private void _SetChildScript (int i) {
		_child = _units.GetChild (i);
		_unitScript = _child.gameObject.GetComponent<Unit> ();
	}
}
