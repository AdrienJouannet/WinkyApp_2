using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public GameObject actionPanel; //Parent Panel that will recieve action item
	private GameObject _newObj;
	private ActionPanelManager _actionScript;

	void Start()
	{
		_actionScript = actionPanel.GetComponent<ActionPanelManager>();
	}

	private void _CreateNewActionItem()
	{
		_newObj = new GameObject(); //Create the GameObject
		Image NewImage = _newObj.AddComponent<Image>(); //Add the Image Component script
		NewImage.sprite = this.gameObject.GetComponent<Image>().sprite; //Set the Sprite of the Image Component on the new GameObject
		_newObj.GetComponent<RectTransform>().SetParent(this.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
		_newObj.transform.localScale = new Vector3(1, 1, 1); //Set the scale to (1, 1, 1) to fit on different resolutions
		_newObj.name = this.name;
		_newObj.tag = "ActionPanel";
		_newObj.AddComponent<ManageActions> (); //Add ManageActions script that allows to delete and move actions
	}

	private void _AddColliderToActionItem()
	{
		Rect rect = this.GetComponent<RectTransform> ().rect;
		_newObj.GetComponent<RectTransform> ().SetParent (actionPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
		_newObj.AddComponent<BoxCollider>();
		_newObj.GetComponent<BoxCollider> ().size = new Vector3 (rect.height, rect.width, 0);//add collider to use OverlapBox
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_actionScript.GetNumberOfActions () > 0)
		{
			_CreateNewActionItem ();
			_AddColliderToActionItem ();
			_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () - 1);
		}
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		_CreateNewActionItem ();
//		GameObject.Find ("Main Camera Top View").GetComponent<MoveCameraInGame> ().SetMoveAllowed (false);
	}

	public void OnDrag(PointerEventData eventData)
	{
		_newObj.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		int siblingIndex;
		GameObject dropZone;

//		GameObject.Find ("Main Camera Top View").GetComponent<MoveCameraInGame> ().SetMoveAllowed (true);
		if ((dropZone = _actionScript.GetDropZone(eventData, _newObj)) != null)
		{
			_AddColliderToActionItem ();
			if (dropZone != actionPanel)//if an item is dropped on an existing action, replace it
			{
				siblingIndex = dropZone.transform.GetSiblingIndex ();
				Destroy (dropZone);
				_newObj.transform.SetSiblingIndex (siblingIndex);
			}
			else if (_actionScript.GetNumberOfActions () > 0)//else create an action with dropped item only if actions are > 0
			{
				_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () - 1);

				GameObject siblingAction = _actionScript.GetSiblingActionOnRight (_newObj);
				if (siblingAction)//if an item is dropped on the left of an action, then move all the child to insert the item
				{
					siblingIndex = siblingAction.transform.GetSiblingIndex ();
					_actionScript.UpdateSiblingIndexOfChildren (siblingIndex, 1);
					_newObj.transform.SetSiblingIndex (siblingIndex);
				}
			}
			else
				Destroy (_newObj);
		}
		else
			Destroy (_newObj);
	}
}
