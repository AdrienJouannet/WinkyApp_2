using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsPanelManager : MonoBehaviour {

	private UnitEditor _unitEditorScript;
	private e_ActionType[] _actionsAvailable;
	[SerializeField]
	private Transform _unitType;


	void Start()
	{
		int children;
		_unitEditorScript = GameObject.Find ("MapEditor").GetComponent<UnitEditor> ();
		_actionsAvailable = _unitEditorScript.GetActionsAvailable (_unitType.name);

		children = this.transform.childCount;
		for (int i = 0; i < children; i++)
		{
			if (!_isAvailable (this.transform.GetChild (i).name))
				this.transform.GetChild (i).gameObject.SetActive (false);
		}


	}

	private bool _isAvailable(string actionName)
	{
		foreach (e_ActionType action in _actionsAvailable)
		{
			if (action.ToString () == actionName)
				return (true);
		}
		return (false);
	}

}
