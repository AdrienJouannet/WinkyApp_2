using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMode : MonoBehaviour {

	private SetActive _setActive;

	private GameObject _enemies;
	private MovableScript _movableScript;
//	private ChangeView _changeViewScript;
	private GameObject _allunits;

	void Start () {
		_setActive = GameObject.Find ("Play (panel)").transform.GetComponent<SetActive> ();
		_enemies = this.transform.GetChild (0).gameObject;
		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
//		_changeViewScript = GameObject.Find ("Main Camera Top View").GetComponent<ChangeView>();
		_allunits = GameObject.Find ("Units");
	}

	private void _ResetAllUnits()
	{
		foreach (Transform unit in _allunits.transform)
		{
			if (unit.gameObject.activeInHierarchy == false) {
				unit.gameObject.SetActive (true);
			}
			_ResetPos (unit);
			_ResetColorOfLastAction (unit);
			_ResetRange (unit);
//			_ResetActionList (unit);
		}
	}

	private void _ResetPos(Transform unit)
	{
		Unit unitScript = unit.GetComponent<Unit> ();
		unit.transform.position = unitScript.GetInitialPosition ();
		unit.transform.eulerAngles = unitScript.GetInitialRotation ();
//		StepByStep stepByStepScript = unit.gameObject.GetComponentInChildren<StepByStep> (true);
//		stepByStepScript.ResetStep ();
	}

	private void _ResetColorOfLastAction(Transform unit)
	{
		UnitAction unitActionScript = unit.GetComponent<UnitAction> ();
		if (unitActionScript.GetCurrentAction ())
		{
			unitActionScript.RemoveColor (unitActionScript.GetCurrentAction ());
		}
		unitActionScript.SetNumAction (0);

	}

	private void _ResetRange (Transform unit) {
		AdjustRange adjustRange = unit.GetComponent<AdjustRange> ();
		adjustRange.ResetRange ();
	}

//	public void ResetPosOfAllUnits()
//	{
//		foreach (Transform unit in _allunits.transform)
//		{
//			StepByStep stepByStepScript = unit.gameObject.GetComponentInChildren<StepByStep> (true);
//			stepByStepScript.ResetStep ();
//			_ResetActionList (unit);
//		}
//	}

//	private void _ResetActionList(Transform unit)
//	{
//		
//		Transform actionList = unit.Find("ActionList");
//		int nbActions = actionList.transform.childCount;
//		for (int i = 0; i < nbActions; i++) {
//			Destroy (actionList.transform.GetChild (i).gameObject);
//		}
//		unit.GetComponent<UnitAction> ().SetNumAction (0);
//	}

	public void ReturnToDefense () {
		if (_setActive)
			_setActive.SetActiveGO ();
		_GetDefenseMode ();
	}

	public void RelaunchWave () {
		_GetDefenseMode ();
		_movableScript.PlayGame ();
	}

	private void _GetDefenseMode () {
		_movableScript.StopGame ();
		int nbEnemies = _enemies.transform.childCount;
//		Debug.Log (nbEnemies);
		for (int i = 0; i < nbEnemies; i++) {
			Destroy (_enemies.transform.GetChild (i).gameObject);
		}
		_ResetAllUnits ();
	}
}
