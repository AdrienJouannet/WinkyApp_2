using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ActionPanelManager : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private int	_actions = 3;

	public void SetNumberOfActions(int nb)
	{
		_actions = nb;
	}

	public int GetNumberOfActions()
	{
		return (_actions);
	}

	public void UpdateSiblingIndexOfChildren(int siblingIndex, int updateNumber)
	{
		int children = this.gameObject.transform.childCount - 2;
		for (int i = children; i >= siblingIndex; i--)
			this.gameObject.transform.GetChild (i).SetSiblingIndex(i + updateNumber);
	}

	public GameObject GetSiblingActionOnLeft(GameObject draggedAction)//Detect if an item is dropped on the right of an action (with the size of the action as area detection)
	{
		Collider[] hitColliders = Physics.OverlapBox(draggedAction.transform.position, new Vector3(40, 40, 0));
		foreach (Collider col in hitColliders)
		{
			if (col.gameObject.transform.position.x < draggedAction.transform.position.x)
				return col.gameObject;
		}
		return null;
	}

	public GameObject GetSiblingActionOnRight(GameObject draggedAction)//Detect if an item is dropped on the left of an action (with the size of the action as area detection)
	{
		Collider[] hitColliders = Physics.OverlapBox(draggedAction.transform.position, new Vector3(40, 40, 0));
		foreach (Collider col in hitColliders)
		{
			if (col.gameObject.transform.position.x > draggedAction.transform.position.x)
				return col.gameObject;
		}
		return null;
	}

	public GameObject GetDropZone(PointerEventData eventData, GameObject draggedAction)
	{
		List<RaycastResult> results = new List<RaycastResult> ();

		EventSystem.current.RaycastAll (eventData, results);
		for (int i = 0; i < results.Count; i++)
		{
			if ((results [i].gameObject.tag == "ActionPanel" || results [i].gameObject == this.gameObject) &&
				results [i].gameObject != draggedAction)
				return (results [i].gameObject);
		}
		return (null);
	}
}
