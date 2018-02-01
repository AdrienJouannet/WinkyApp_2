using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cette classe se charge de garder en memoire l'unit sur laquelle on a clique
// Si une unite etait precedemment enregistree, fermer ses panels et mettre a jour

public class ActiveUnit : MonoBehaviour {

	private Transform _activeUnit = null;

	private GameObject _unitCanvas;
	private MovableScript _movableScript;

	// Use this for initialization
	void Start () {
		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
	}

	public void SetActiveUnit (Transform unit) {
//		Si premier clic
		if (!_activeUnit) {
			GameObject.Find ("NoUnitPanel").SetActive (false);
			_SetCanvas (unit);
		}
//		Si clic sur une nouvelle unite
		else if (unit != _activeUnit) {
			_activeUnit.gameObject.GetComponent<UnitClic> ().SetUnactive ();
			_ChangeUnitCanvas (false);
			_SetCanvas (unit);
		}
	}

	private void _SetCanvas (Transform unit) {
		_activeUnit = unit;
		_unitCanvas = unit.Find ("UnitCanvas").gameObject;
		_ChangeUnitCanvas (true);
	}

	private void _ChangeUnitCanvas (bool state) {
		_unitCanvas.SetActive (state);
		if (_movableScript.GetIsPlaying ()) {
			_unitCanvas.transform.Find ("Commands Panel").gameObject.SetActive (false);
		}
		else {
			_unitCanvas.transform.Find ("Commands Panel").gameObject.SetActive (true);
		}
	}

	public void  ChangeStateOfCommandPanel(bool state)
	{
		if (_activeUnit)
		{
			Transform unitCanvas = _activeUnit.Find ("UnitCanvas");
			unitCanvas.Find ("Commands Panel").gameObject.SetActive (state);
			unitCanvas.Find ("ResetBtn").gameObject.SetActive (state);
		}
	}
}
