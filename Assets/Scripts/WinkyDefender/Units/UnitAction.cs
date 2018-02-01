using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitAction : MonoBehaviour {

	public Transform		actionList;//A supprimer car pas utile desormais. Supprimer aussi le GO

	private int				_numAction = 0;
	private Moves_Default	_moveScript;
	public Transform 		actionPanel;
	private Transform 		_currentAction;

	public void Start () {
		_moveScript = this.transform.GetComponent<Moves_Default> ();
	}

	public void SetNumAction(int nb)
	{
		_numAction = nb;
	}

	private void _AddColor(Transform action)
	{
		action.GetComponent<Image> ().color = Color.yellow;
	}

	public void RemoveColor(Transform action)
	{
		action.GetComponent<Image> ().color = Color.white;
	}

	public Transform GetCurrentAction()
	{
		return (_currentAction);
	}

	public void SetCurrentAction(Transform action)
	{
		_currentAction = action;
	}

	public IEnumerator NextAction (MovableScript script)
	{
		if (_numAction < actionPanel.childCount)
		{
			if (_currentAction)
				RemoveColor (_currentAction);
			_currentAction = actionPanel.GetChild (_numAction);
			_AddColor (_currentAction);
			yield return StartCoroutine (_moveScript.MoveUnit (_currentAction.name, script));
			_numAction++;
		}
		else if (_currentAction)
			RemoveColor (_currentAction);
	}

	public void StopMovement()
	{
		StopAllCoroutines ();
		_moveScript.StopMovement ();
	}

//	public IEnumerator NextAction (MovableScript script) {
//		if (_numAction < actionList.childCount) {
//			yield return StartCoroutine(_moveScript.MoveUnit (actionList.GetChild (_numAction).name, script));
//			_numAction++;
//		}
//	}
//
//	public bool NextAction2 () {
//		if (_numAction < actionList.childCount) {
//			_moveScript.MoveUnit (actionList.GetChild (_numAction).name);
//			_numAction++;
//		} else {
//			this.gameObject.GetComponent<InMoveState> ().SetInMove (false);
//		}
//		return (true);
//	}
}
