using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleManager : MonoBehaviour {

	private bool _debug = false;

	public GameObject changeSpeed;
	public GameObject pauseBtn;

	private bool _pause = false;
	private bool _fastMode = false;
	private bool _menuOpen = false;
	private float _timescale;
//	private MovableScript _movableScript;


	void Start () {
//		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
	}

	public void ChangeSpeed () {
		if (_pause)
			return;
		_fastMode = !_fastMode;
		if (_fastMode) {
			Time.timeScale = 2;
//			_ActiveSecond (changeSpeed);
			_ChangeColor (changeSpeed, Color.black);
		}
		else {
			Time.timeScale = 1;
//			_ActiveFirst (changeSpeed);
			_ChangeColor (changeSpeed, Color.grey);
		}
		if (_debug)
			Debug.Log ("Speed change to : " + Time.timeScale);
	}

	public void ChangePause () {
		_pause = !_pause;
		if (_pause) {
			_timescale = Time.timeScale;
			Time.timeScale = 0;
			_ActiveSecond (pauseBtn);
			if (_debug)
				Debug.Log ("Pause. TimeScale = 0");
		}
		else {
			Time.timeScale = _timescale;
			_ActiveFirst (pauseBtn);
		}
	}

	public void OpenMenu () {
		if (_pause == false)
			ChangePause ();
		else
			_menuOpen = true;
//		_movableScript.SetOpenMenu ();
		this.transform.parent.GetComponent<Canvas> ().sortingOrder = 2;
	}

	public void CloseMenu () {
//		_movableScript.SetOpenMenu ();
		this.transform.parent.GetComponent<Canvas> ().sortingOrder = 0;
	}

	public void ResetPause () {
		if (_pause && _menuOpen == false)
			ChangePause ();
		if (_menuOpen)
			_menuOpen = false;
	}

	public void QuitLevel () {
		Time.timeScale = 1;
		if (_debug)
			Debug.Log ("Speed change to : " + Time.timeScale);
	}

	private void _ChangeColor (GameObject toChange, Color color) {
		toChange.transform.GetChild (0).GetComponent<Image> ().color = color;
	}

	private void _ActiveFirst (GameObject button) {
		_ChangeImage (button.transform.GetChild (0).gameObject, button.transform.GetChild (1).gameObject);
	}

	private void _ActiveSecond (GameObject button) {
		_ChangeImage (button.transform.GetChild (1).gameObject, button.transform.GetChild (0).gameObject);
	}

	private void _ChangeImage (GameObject activeTrue, GameObject activeFalse) {
		activeFalse.SetActive (false);
		activeTrue.SetActive (true);
	}
}
