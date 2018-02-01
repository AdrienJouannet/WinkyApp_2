using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClic : MonoBehaviour {

//	public GameObject actionPanel;

	public GameObject selectedTile;
	public GameObject range;

	private GameObject _units;
	private ActiveUnit _unitsScript;
	private PlaceUnitsPanel _placeUnitsPanelScript;
	private BuildManager _buildManager;
	private GameObject _tutoCanvas;
	private DisplayTutorial _tutoCanvasScript;

//	[System.NonSerialized]
	private bool _isActive = false;

	// Use this for initialization
	void Start () {
		_units = GameObject.Find ("Units");
		_unitsScript = _units.GetComponent<ActiveUnit> ();
		_buildManager = BuildManager.instance;
		_tutoCanvas = GameObject.Find ("Tuto (canvas)");
		if (_tutoCanvas)
			_tutoCanvasScript = _tutoCanvas.GetComponent<DisplayTutorial> ();
	}

	// On Unit Mouse Down
	public void OnMouseDown() {
		if (_tutoCanvasScript)
			_tutoCanvasScript.DisplayNextTutorial ("Unit");
		if (!_isActive) {
			selectedTile.SetActive (true);
			range.SetActive (true);
			_isActive = !_isActive;
			if (_placeUnitsPanelScript = _buildManager.GetUnitType ())
			{
				_placeUnitsPanelScript.RemoveColor (_placeUnitsPanelScript);
				_placeUnitsPanelScript.DeselectUnitToBuild ();
			}
			_unitsScript.SetActiveUnit (this.gameObject.transform);
		}
	}

	public void SetUnactive () {
		_isActive = false;
		selectedTile.SetActive (false);
		range.SetActive (false);
	}

//	public void ResetStepByStep () {
//		actionPanel.GetComponent<StepByStep> ().OnPlay ();
//	}
}
