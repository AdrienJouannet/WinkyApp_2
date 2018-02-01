using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepByStep : MonoBehaviour {

	public GameObject	unit;
//	private Vector3		_startingPosition;
//	private Vector3		_startingRotation;

	private Color		_imageColor;
//	private Image 		_lastAction = null;

	public Color 		defaultColor;
	public Color		selectionColor;
	private GameObject	_action;
	private Unit		_unitScript;

	public Transform	StepBtn;

//	La variable est publique pour pouvoir etre reinitialisee lorsqu'on appuie sur Play
//	Elle est nonSerialized pour ne pas apparaitre dans l'inspector
	[System.NonSerialized]
	public	int numAction = 0;
	private int maxAction;

	private void Start () {
//		maxAction = this.transform.childCount;
		defaultColor = Color.white;
		selectionColor = Color.yellow;
		_unitScript = unit.GetComponent<Unit> ();
//		Debug.Log ("qlors = " + _unitScript);
//		_InitPosition ();
	}

	public void SetMaxAction(int nb)
	{
		maxAction = nb;
	}

//	Conserve la position initiale de l'objet
	private void _InitPosition () {
//		_startingPosition = unit.transform.position;
//		_startingRotation = unit.transform.eulerAngles;
	}

	public void ResetStep ()
	{
		numAction = 0;
		if (!_unitScript)
			_unitScript = unit.GetComponent<Unit> ();
		unit.transform.position = _unitScript.GetInitialPosition();
		unit.transform.eulerAngles = _unitScript.GetInitialRotation();
		if (_action)
			_action.GetComponent<Image> ().color = defaultColor;
	}

	public void NextStep() {
		StartCoroutine (_DoNextStep());
	}

	private IEnumerator _DoNextStep() {
		maxAction = this.gameObject.transform.childCount;
		if (numAction == 0)
			_InitPosition ();
		StepBtn.gameObject.SetActive (false);
		//		Si on a atteint la derniere action, replace l'objet a sa position initiale
		if (numAction == maxAction)
		{
			ResetStep ();
		}
		else
		{
			if (numAction != 0)
				_action.GetComponent<Image> ().color = defaultColor;

			_action = this.transform.GetChild (numAction).gameObject;
			_action.GetComponent<Image> ().color = selectionColor;
			yield return (StartCoroutine(unit.transform.GetComponent<Moves_Default> ().MoveUnit (_action.name, null)));
			numAction++;
		}
		StepBtn.gameObject.SetActive (true);
	}

//	//	Cette fonction permet de reinitialiser les valeurs
//	public void ResetStep_old () {
//		numAction = 0;
//		if (_lastAction != null)
//			_lastAction.color = _imageColor;
//		_lastAction = null;
//		unit.transform.position = _startingPosition;
//		unit.transform.eulerAngles = _startingRotation;
//	}
//
//	public void NextStep_old () {
//		if (_lastAction == null)
//			_InitPosition ();
//
//		StepBtn.gameObject.SetActive (false);
////		Si on a atteint la derniere action, replace l'objet a sa position initiale
//		if (numAction == maxAction)
//			ResetStep ();
//		else {
//			Transform actionGO = this.transform.GetChild (numAction);
//			Image actionImage = actionGO.GetChild (0).GetComponent<Image> ();
//
////			Si aucune action est trouvee, on passe a la suivante
//			if (actionImage.name == "Empty") {
//				numAction++;
//				NextStep();
//				return;
//			}
//
//			if (_lastAction == null)
//				_imageColor = actionImage.color;
//			else
//				_lastAction.color = _imageColor;
//			
////			Sauvegarde de l'action actuelle
////			Changement de la couleur
//			_lastAction = actionImage;
//			actionImage.color = Color.yellow;
//
////			Mouvement
//			unit.transform.GetComponent<Moves_Default> ().MoveUnit (actionImage.name);
//
//			numAction++;
//		}
//		StepBtn.gameObject.SetActive (true);
//	}

	public bool OnPlay () {
		if (numAction != 0) {
			ResetStep ();
			return (true);
		}
		return (false);
	}
}
