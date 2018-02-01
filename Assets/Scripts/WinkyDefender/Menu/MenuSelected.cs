using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelected : MonoBehaviour {

	public	GameObject mainMenu;
	private GameObject _actualMenu;
	private bool change = false;

	public void SetActualMenu (GameObject menu) {
		_actualMenu = menu;
		change = true;
	}

	void Start () {
		if (change && _actualMenu != mainMenu) {
			mainMenu.SetActive (false);
			_actualMenu.SetActive (true);
			change = false;
		}
	}
}
