using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicOnSpawn : MonoBehaviour {

	private GameObject _spawnInfo;
	private bool _justClicked = false;
	private GameObject _tutoCanvas;
	private DisplayTutorial _tutoCanvasScript;


	// Use this for initialization
	void Start () {
		_spawnInfo = GameObject.Find ("SpawnInfo");
		_tutoCanvas = GameObject.Find ("Tuto (canvas)");
		if (_tutoCanvas)
			_tutoCanvasScript = _tutoCanvas.GetComponent<DisplayTutorial> ();
	}
	
	// On Spawn Mouse Down
	public void OnMouseDown () {
//		Debug.Log ("Spawn Clic!");
		if (_tutoCanvasScript)
			_tutoCanvasScript.DisplayNextTutorial ("Spawn");
		foreach (Transform spawn in _spawnInfo.transform) {
			if (spawn.name == this.name) {
				spawn.gameObject.SetActive (true);
				_justClicked = true;
				return;
			}
		}
	}

	public bool GetJustClicked () {
		return (_justClicked);
	}

	public void SetJustClicked () {
		_justClicked = false;
	}
}
