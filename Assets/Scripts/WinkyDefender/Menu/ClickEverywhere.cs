using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEverywhere : MonoBehaviour {

	private GameObject _spawnInfo;
	private GameObject _spawns;
	private GameObject _tutoCanvas;
	private DisplayTutorial _tutoCanvasScript;

	// Use this for initialization
	void Start () {
		_spawnInfo = GameObject.Find ("SpawnInfo");
		_spawns = GameObject.Find ("Spawns");
		_tutoCanvas = GameObject.Find ("Tuto (canvas)");
		if (_tutoCanvas)
			_tutoCanvasScript = _tutoCanvas.GetComponent<DisplayTutorial> ();
	}

	private void _CloseSpawnInfo () {
		foreach (Transform spawnInfo in _spawnInfo.transform) {
			if (spawnInfo.gameObject.activeInHierarchy) {
				GameObject spawn = null;
				foreach (Transform spawns in _spawns.transform) {
					if (spawns.name == spawnInfo.name) {
						spawn = spawns.gameObject;
						break;
					}
				}
				if (spawn == null)
					return;
				ClicOnSpawn clicOnSpawn = spawn.GetComponent<ClicOnSpawn> ();
				if (clicOnSpawn.GetJustClicked ())
					clicOnSpawn.SetJustClicked ();
				else
					spawnInfo.gameObject.SetActive (false);
			}
		}
	}
	
	public void OnClickAnywhere () {
		if (_tutoCanvasScript) {
//			Debug.Log ("Tuto Canvas next Tuto");
			_tutoCanvasScript.DisplayNextTutorial ("");
		}
		_CloseSpawnInfo ();
	}
}
