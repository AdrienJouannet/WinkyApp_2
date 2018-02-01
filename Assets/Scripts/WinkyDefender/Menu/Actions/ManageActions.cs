using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ManageActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	private GameObject _actionPanel;
	private Vector3 _initialPosition;
	private ActionPanelManager _actionScript;
	private bool _isDragged;
	private MovableScript _movableScript;


	void Start()
	{
		_actionPanel = GameObject.Find ("ActionPanel");
		_actionScript = _actionPanel.GetComponent<ActionPanelManager> ();
		_isDragged = false;
		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!_isDragged && !_movableScript.GetIsPlaying())
		{
			Destroy (this.gameObject);
			_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () + 1);
		}
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (!_movableScript.GetIsPlaying ())
		{
			_isDragged = true;
			_initialPosition = this.gameObject.transform.position;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_movableScript.GetIsPlaying ())
			this.gameObject.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GameObject dropZone;
		int siblingIndex;
		if (!_movableScript.GetIsPlaying ())
		{
			if ((dropZone = _actionScript.GetDropZone (eventData, this.gameObject)) != null)
			{
				if (dropZone != _actionPanel)
				{
					siblingIndex = dropZone.transform.GetSiblingIndex ();
					Destroy (dropZone);
					this.gameObject.transform.SetSiblingIndex (siblingIndex);
					_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () + 1);
				}
				else
				{
					GameObject siblingAction;

					if ((siblingAction = _actionScript.GetSiblingActionOnRight (this.gameObject)) &&
					    (siblingIndex = siblingAction.transform.GetSiblingIndex ()) < this.gameObject.transform.GetSiblingIndex ())//if an item is dropped on the left of an action, then move all the child to insert the item
					{
						_actionScript.UpdateSiblingIndexOfChildren (siblingIndex, 1);
						this.gameObject.transform.SetSiblingIndex (siblingIndex);
					}
					else if ((siblingAction = _actionScript.GetSiblingActionOnLeft (this.gameObject)) &&
					         (siblingIndex = siblingAction.transform.GetSiblingIndex ()) > this.gameObject.transform.GetSiblingIndex ())//if an item is dropped on the left of an action, then move all the child to insert the item
					{
						_actionScript.UpdateSiblingIndexOfChildren (siblingIndex, -1);
						this.gameObject.transform.SetSiblingIndex (siblingIndex);
					}
					else
					{
						Transform lastChild = _actionPanel.transform.GetChild (_actionPanel.transform.childCount - 1);

						if (this.gameObject.transform.position.x > lastChild.position.x)
						{
							_actionScript.UpdateSiblingIndexOfChildren (lastChild.GetSiblingIndex (), -1);
							this.gameObject.transform.SetSiblingIndex (lastChild.GetSiblingIndex ());
						}
						else
							this.gameObject.transform.position = _initialPosition;
					}
				}
			}
			else
			{
				_actionScript.SetNumberOfActions (_actionScript.GetNumberOfActions () + 1);
				Destroy (this.gameObject);
			}
			_isDragged = false;
		}
	}

}