using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMode : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject ARCamera;

	private bool state = false;

	// Use this for initialization
	void Start () {
//		Vuforia.VuforiaBehaviour.Instance.enabled = state;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToAR () {
//		MainCamera.SetActive (false);
//		ARCamera.SetActive (true);
		state = !state;
//		Vuforia.VuforiaBehaviour.Instance.enabled = state;
	}
}
