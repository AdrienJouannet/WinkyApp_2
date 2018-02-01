using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameObjectScript : MonoBehaviour {

	public GameObject camera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateEnv (bool state) {
		if (state)
			_TrackingOn ();
		else
			_TrackingOff ();
	}

	private void _TrackingOn () {
		this.transform.GetChild (0).gameObject.SetActive (false);
//		this.transform.position = new Vector3 (0, 0, 0);
	}

	private void _TrackingOff () {
		this.transform.GetChild (0).gameObject.SetActive (true);
//		camera.transform.transform.position = new Vector3 (100, 0, 0);
//		this.transform.position = new Vector3 (0, 100, 0);
	}
}
