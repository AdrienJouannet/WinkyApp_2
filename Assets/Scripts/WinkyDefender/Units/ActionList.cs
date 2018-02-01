using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionList : MonoBehaviour {

	public GameObject actionPanel;

	private int _nbAction;

//	 Use this for initialization
//	void Start () {
//		_nbAction = actionPanel.transform.childCount;
//
//		for (int i = 0; i < _nbAction; i++) {
//			GameObject childObject = new GameObject ("Empty");
//			childObject.transform.position = this.transform.position;
//			childObject.transform.parent = this.transform;
//		}
//	}
//	
//	public void InstantiateAction () {
//		int i = 0;
//
//		foreach (Transform Action in this.actionPanel.transform) {
//			string nameAction = Action.GetChild (0).GetComponent<Image> ().name;
//
//			this.transform.GetChild (i).gameObject.name = nameAction;
//			i++;
//		}
//	}
	public void AddActionsToActionList()
	{
		foreach (Transform action in actionPanel.transform)
		{
			GameObject actionToDo = new GameObject (action.name);
			actionToDo.transform.position = this.transform.position;
			actionToDo.transform.parent = this.transform;
		}
	}
}
