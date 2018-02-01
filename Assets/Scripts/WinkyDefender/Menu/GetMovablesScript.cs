using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMovablesScript : MonoBehaviour {

	private DefenseMode _defenseMode;

	// Use this for initialization
	void Start () {
		_defenseMode = GameObject.Find ("Movables").transform.GetComponent<DefenseMode> ();
	}
	
	public void Relaunch () {
		_defenseMode.RelaunchWave ();
	}

	public void DefenseMode () {
		_defenseMode.ReturnToDefense ();
	}
}
