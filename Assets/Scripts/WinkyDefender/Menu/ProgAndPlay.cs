using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgAndPlay : MonoBehaviour {

	// Public vs [SerializeField] private
//	public Transform progPanel;
//	public Transform commandsPanel;
//	public Text playBtnText;
//	public Transform actionPanel;
//	public GameObject unit;
//	public Transform playBtn;
//
//	private Moves_Default _unitScript;
//
//
//	private void Start ()
//	{
//		_unitScript = unit.transform.GetComponent<Moves_Default> ();
//		Debug.Log ("LAAAAAAAAAAAAAAAAAA");
//
////		Ci-dessous: juste pour la prog et pas avoir besoin de cacher les panels
//		if (progPanel.gameObject.activeInHierarchy == true)
//		{
//			commandsPanel.gameObject.SetActive (true);
//			playBtnText.text = "Play";
//		}
//		else
//			commandsPanel.gameObject.SetActive (false);
////		End
//	}
//
//	public void OnStartGame() {
//		Debug.Log ("ICIIII");
//
////		Method without assign the gameobject in the inspector:
////		Text btnText = GameObject.Find("PlayBtn").GetComponentInChildren<Text> ();
//
//		if (progPanel.gameObject.activeInHierarchy == false &&
//			commandsPanel.gameObject.activeInHierarchy == false)
//		{
//			progPanel.gameObject.SetActive (true);
//			commandsPanel.gameObject.SetActive (true);
//			playBtnText.text = "Play";
//		}
//		else
//		{
//			progPanel.gameObject.SetActive (false);
//			commandsPanel.gameObject.SetActive (false);
//			playBtnText.text = "Actions";
//			playBtn.gameObject.SetActive (false);
//
//			StartCoroutine("PlayGame");
//		}
//	}
//
//	IEnumerator PlayGame()
//	{
//		Debug.Log ("OKKKK");
//
////		Reset le script StepByStep pour la prochaine ouverture
//		if (actionPanel.transform.GetComponent<StepByStep> ().OnPlay ())
//			yield return new WaitForSeconds (0.60f);
//
////		Les actions sont effectuees dans l'ordre du panel
//		foreach (Transform Action in this.actionPanel) {
//
//			string nameAction = Action.GetChild (0).GetComponent<Image> ().name;
//
//			if (_unitScript.MoveUnit(nameAction, null))
//				yield return new WaitForSeconds (0.60f);
//		}
//		playBtn.gameObject.SetActive (true);
//	}
}
