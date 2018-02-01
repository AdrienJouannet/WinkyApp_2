using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public int WinkyLife = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (WinkyLife == 0) {
			Debug.Log ("Winky died in terrible pain!");
//			UnityEditor.EditorApplication.isPlaying = false;
		}
	}
}
