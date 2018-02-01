using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {

	public GameObject[] activateGO;
	public GameObject[] deactivateGO;

	public void SetActiveGO () {
		_ActivateGO ();
		_DeactivateGO ();
	}

	private void _ActivateGO () {
		foreach (GameObject go in activateGO) {
			go.SetActive (true);
		}
	}

	private void _DeactivateGO () {
		foreach (GameObject go in deactivateGO) {
			go.SetActive (false);
		}
	}
}
