using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTutorial : MonoBehaviour {

	[System.Serializable]
	public class BtnToUnactive
	{
		public Button[] noClicButton;
		public int activeNb;
	}

	[System.Serializable]
	public class UnitsToUnactive
	{
		[HideInInspector] 
		public GameObject units;
		public int activeUnitNb;
		public int activeCanvasNb;
	}

	[System.Serializable]
	public class SpawnsToUnactive
	{
		[HideInInspector] 
		public GameObject spawns;
		public int activeNb;
	}

	[System.Serializable]
	public class WaitKey
	{
		public int waitNB;
		public string key;
	}

	public BtnToUnactive[] btnToUnactive;
	public UnitsToUnactive unitsToUnactive;
	public SpawnsToUnactive spawnsToUnactive;
	public WaitKey[] waitKey;

	private WaitKey _actualKey;
	private int _keyNb;
	private int _child;
	private int _nbChilds;
	private GameObject _screenCanvas;

	void Start ()
	{
		_child = 0;
		_nbChilds = this.transform.childCount;
		_screenCanvas = GameObject.Find ("Screen (canvas)");
		unitsToUnactive.units = GameObject.Find ("Units");
		spawnsToUnactive.spawns = GameObject.Find ("Spawns");
		_keyNb = 0;
		if (waitKey.Length > 0)
			_actualKey = waitKey [_keyNb];
		foreach (BtnToUnactive btn in btnToUnactive) {
			_ChangeBtnState (btn, false);
		}
		StartCoroutine (_WaitStart ());
	}

	private IEnumerator _WaitStart () {
		yield return new WaitForSeconds (0.01f);
		if (unitsToUnactive.activeUnitNb != 0) {
			_ChangeUnitState (false);
			_ChangeUnitCanvasOrder (-2);
		}
		if (spawnsToUnactive.activeNb != 0)
			_ChangeSpawnState (false);
	}

	private bool _CanDisplay(string key)
	{
		if (waitKey.Length > 0 && _child == _actualKey.waitNB) {
			if (key == _actualKey.key) {
				_keyNb++;
				if (_keyNb < waitKey.Length)
					_actualKey = waitKey [_keyNb];
				return (true);
			} else
				return (false);
		} else {
			return (true);
		}
	}

	public void DisplayNextTutorial(string key)
	{
		if (_child < _nbChilds && _CanDisplay (key))
		{
			_screenCanvas.SetActive (false);
			this.transform.GetChild (_child).gameObject.SetActive (false);
			_child++;
			if (_child < _nbChilds)
				this.transform.GetChild (_child).gameObject.SetActive (true);
			_AdjustInteractibles ();
			_screenCanvas.SetActive (true);
		}
	}

	private void _AdjustInteractibles () {
		if (_child == spawnsToUnactive.activeNb)
			_ChangeSpawnState (true);
		if (_child == unitsToUnactive.activeUnitNb)
			_ChangeUnitState (true);
		if (_child == unitsToUnactive.activeCanvasNb)
			_ChangeUnitCanvasOrder (1);
		foreach (BtnToUnactive btn in btnToUnactive) {
			if (_child == btn.activeNb)
				_ChangeBtnState (btn, true);
		}
	}

	private void _ChangeBtnState (BtnToUnactive btn, bool state) {
		for (int i = 0; i < btn.noClicButton.Length; i++) {
			btn.noClicButton [i].interactable = state;
		}
	}

	private void _ChangeUnitState (bool state) {
		foreach (Transform unit in unitsToUnactive.units.transform) {
				unit.GetComponent<CapsuleCollider> ().enabled = state;
			}
	}

	private void _ChangeUnitCanvasOrder (int order) {
		foreach (Transform unit in unitsToUnactive.units.transform) {
				unit.Find ("UnitCanvas").GetComponent<Canvas> ().sortingOrder = order;
			}
	}

	private void _ChangeSpawnState (bool state) {
		foreach (Transform spawn in spawnsToUnactive.spawns.transform) {
				spawn.GetComponent<BoxCollider> ().enabled = state;
			}
	}
}
