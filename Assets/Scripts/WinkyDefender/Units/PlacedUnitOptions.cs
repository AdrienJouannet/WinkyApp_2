using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedUnitOptions : MonoBehaviour {

	private GameObject _unitPanel;
	private BuildManager _buildManager;
	private Unit _unitScript;
	private Moves_Default _movesDefault;
	private bool _canRotate;

	// Use this for initialization
	void Start () {
		_unitPanel = GameObject.Find ("UnitPanel");
		_buildManager = BuildManager.instance;
		_movesDefault = this.gameObject.GetComponent<Moves_Default> ();
		_unitScript = this.gameObject.GetComponent<Unit> ();
		_canRotate = true;
	}

    public void TurnRight()
    {
		if (_canRotate)
			StartCoroutine(_DoTurnRight());
    }
	private IEnumerator _DoTurnRight()
	{
		_canRotate = false;
//		Debug.Log ("Turn Right ->");
		yield return(StartCoroutine(_movesDefault.MoveUnit ("Right", null)));
		_unitScript.SetInitialRotation (this.gameObject.transform.eulerAngles);
		_canRotate = true;
	}

	public void TurnLeft()
	{
		if (_canRotate)
			StartCoroutine (_DoTurnLeft ());
	}

	private IEnumerator _DoTurnLeft()
    {
		_canRotate = false;
//		Debug.Log ("Turn Left ->");
		yield return(StartCoroutine (_movesDefault.MoveUnit ("Left", null)));
		_unitScript.SetInitialRotation (this.gameObject.transform.eulerAngles);
		_canRotate = true;
    }

    public void Remove () {
		Debug.Log ("GO = " + gameObject);
		_updateUnitLeft (gameObject.tag);
        Destroy(gameObject);
		_unitPanel.SetActive (true);
		_buildManager.SetTurretToBuild (null);
	}

	private bool _updateUnitLeft (string tag) {
//		Debug.Log(tag + " destroyed!");
		foreach (Transform unit in _unitPanel.transform)
		{
			Debug.Log ("unit = " + unit);
			if (unit.name == tag)
			{
				PlaceUnitsPanel setUnitLeft = unit.GetComponent<PlaceUnitsPanel> ();
				setUnitLeft.SetUnitLeft (setUnitLeft.GetUnitLeft () + 1);
				return (true);
			}
		}
		return (false);
	}
}
