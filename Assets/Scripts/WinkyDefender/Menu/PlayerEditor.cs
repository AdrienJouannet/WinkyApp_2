using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEditor : MonoBehaviour {

	public GameObject mainMenu;

	private string _activeMenu;
	private GameObject[] _playerEditor;

	private bool _debug = false;

	// Use this for initialization
	void Start () {
		_activeMenu = mainMenu.transform.name;
		DontDestroyOnLoad(this.gameObject);
		_playerEditor = GameObject.FindGameObjectsWithTag("PlayerEditor");
		if (_playerEditor.Length > 1) {
			_SaveAndDestroy ();
		}
		if (_debug)
			Debug.Log (_activeMenu);
		mainMenu.transform.GetComponent<ActiveCorrectMenu> ().ActiveMenu (_activeMenu);
	}

	public void SetActiveMenu (GameObject menu) {
		_activeMenu = menu.transform.name;
		if (_debug)
			Debug.Log (_activeMenu);
	}

	public string GetActiveMenu () {
		return (_activeMenu);
	}

	public void SaveActiveMenu (string name) {
		_activeMenu = name;
	}

	private void _SaveAndDestroy () {
		PlayerEditor toSave;
		PlayerEditor toSet;

		toSave = _playerEditor [0].transform.GetComponent <PlayerEditor> ();
		toSet = _playerEditor [1].transform.GetComponent <PlayerEditor> ();
		toSet.SaveActiveMenu (toSave.GetActiveMenu ());

		Destroy (_playerEditor[0]);
	}
}
