using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetActions : MonoBehaviour {

	private NewActionPanelManager _actionScript;

	void Start()
	{
		_actionScript = GameObject.Find ("ActionPanel").GetComponent<NewActionPanelManager> ();
	}

	public void ResetCommands () {
		foreach (Transform action in this.transform) {
			Destroy (action.gameObject);
			_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () + 1);
		}
	}
}
