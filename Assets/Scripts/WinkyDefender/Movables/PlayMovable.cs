using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovable : MonoBehaviour {

	private MovableScript _movableScript;

	private GameObject _tutoCanvas;
	private DisplayTutorial _tutoCanvasScript;

	// Use this for initialization
	void Start () {
		_movableScript = GameObject.Find ("Movables").transform.GetComponent<MovableScript> ();
		_tutoCanvas = GameObject.Find ("Tuto (canvas)");
		if (_tutoCanvas)
			_tutoCanvasScript = _tutoCanvas.GetComponent<DisplayTutorial> ();
	}
	
	public void PlayScript () {
		if (_tutoCanvasScript)
			_tutoCanvasScript.DisplayNextTutorial ("Play");
		_movableScript.PlayGame ();
	}
}
