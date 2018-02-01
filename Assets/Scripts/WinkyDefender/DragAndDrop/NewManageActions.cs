using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class NewManageActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	private GameObject _actionPanel;
	private bool _isDragged;
	private MovableScript _movableScript;
    private GameObject _tmpAction;
    private NewActionPanelManager _actionPanelManagerScript;//a renommer le type
    private GameObject _actionHit;
    private Unit _unitParentScript;
	private bool _replace;
	private bool _placeAfter;
	private Vector3 _tmpPos;


	void Start()
	{
        _actionPanel = this.transform.parent.gameObject;
        _actionPanelManagerScript = _actionPanel.GetComponent<NewActionPanelManager> ();
		_isDragged = false;
		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
        _unitParentScript = GetComponentInParent<Unit> ();
		_replace = false;
		_placeAfter = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!_isDragged && !_movableScript.GetIsPlaying())
		{
            _actionPanel = this.gameObject.transform.parent.gameObject;
            _actionPanelManagerScript = _actionPanel.GetComponent<NewActionPanelManager> ();
            _unitParentScript.SetActiveActionPanel (_actionPanel);
            _actionPanelManagerScript.SetNumberOfActions (_actionPanelManagerScript.GetNumberOfActions () + 1);
            Destroy (this.gameObject);
		}
	}

    public GameObject GetDropActionPanel(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult> ();

        EventSystem.current.RaycastAll (eventData, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results [i].gameObject.name == "ActionPanel")
                return (results [i].gameObject);
        }
        return (null);
    }

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (!_movableScript.GetIsPlaying ())
		{
			_isDragged = true;
            _actionPanelManagerScript.SetNumberOfActions (_actionPanelManagerScript.GetNumberOfActions () + 1);
			this.transform.GetComponent<RectTransform> ().SetParent (_actionPanel.transform.parent.transform.parent.transform.parent);
//			this.transform.GetComponent<RectTransform> ().SetParent (_tmpParent);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_movableScript.GetIsPlaying ()) {
			this.transform.position = Input.mousePosition;
			_tmpPos = this.transform.position;
			_tmpPos.y += this.GetComponent<RectTransform> ().rect.height;
			this.transform.position = _tmpPos;
			if (_actionPanel = GetDropActionPanel (eventData)) {
				_actionPanelManagerScript = _actionPanel.GetComponent<NewActionPanelManager> ();
				if (_actionPanelManagerScript.GetNumberOfActions () > 0) {
					_replace = false;
					_actionPanelManagerScript.SetAllPipesTransparent ();
					if (_actionHit)
						_actionHit.GetComponent<Image> ().color = Color.white;
					if (_actionHit = _actionPanelManagerScript.GetActionHit (this.transform, ref _placeAfter, ref _replace)) {
						if (_replace) {
							_actionHit.GetComponent<Image> ().color = Color.red;
							if (_tmpAction)
								Destroy (_tmpAction);
						} else if (!_placeAfter) {
							_actionPanelManagerScript.ChangePipeAlpha (false, _actionHit.transform.GetSiblingIndex ());
							if (_tmpAction)
								Destroy (_tmpAction);
						} else {

							int lastChildIndex;
							if (_tmpAction)
								lastChildIndex = _actionPanel.transform.childCount - 2;
							else
								lastChildIndex = _actionPanel.transform.childCount - 1;
							if (_actionHit.transform != _actionPanel.transform.GetChild (lastChildIndex)) {

								_actionPanelManagerScript.ChangePipeAlpha (false, _actionHit.transform.GetSiblingIndex () + 1);
								if (_tmpAction)
									Destroy (_tmpAction);
							} else if (!_tmpAction)
								_tmpAction = _actionPanelManagerScript.CreateTmpAction (this.GetComponent<Image> ());
						}
					} else {
						if (!_tmpAction)
							_tmpAction = _actionPanelManagerScript.CreateTmpAction (this.GetComponent<Image> ());
					}

				} else {
					_replace = true;
					if (_actionHit)
						_actionHit.GetComponent<Image> ().color = Color.white;
					if (_actionHit = _actionPanelManagerScript.GetActionToReplace (this.transform)) {
						_actionHit.GetComponent<Image> ().color = Color.red;
					}
				}
			} else {
				if (_actionPanelManagerScript)
					_actionPanelManagerScript.SetAllPipesTransparent ();                
				if (_tmpAction)
					Destroy (_tmpAction);
				else if (_actionHit)
					_actionHit.GetComponent<Image> ().color = Color.white;

			}
		}
	}

    public void OnEndDrag(PointerEventData eventData)
	{
		if (!_movableScript.GetIsPlaying ()) {
			if (_actionPanel) {
				this.gameObject.GetComponent<RectTransform> ().SetParent (_actionPanel.transform);
				_unitParentScript.SetActiveActionPanel (_actionPanel);

				if (_replace) {
					if (_actionHit) {
						_actionHit.GetComponent<Image> ().sprite = this.GetComponent<Image> ().sprite;
						_actionHit.GetComponent<Image> ().color = Color.white;
						_actionHit.name = this.name;
					}
					Destroy (this.gameObject);
				} else {
					_actionPanelManagerScript.SetNumberOfActions (_actionPanelManagerScript.GetNumberOfActions () - 1);
					if (_tmpAction) {
						this.transform.SetSiblingIndex (_tmpAction.transform.GetSiblingIndex ());
						Destroy (_tmpAction);
					} else {
						if (!_placeAfter)
							this.transform.SetSiblingIndex (_actionHit.transform.GetSiblingIndex ());
						else
							this.transform.SetSiblingIndex (_actionHit.transform.GetSiblingIndex () + 1);
						_actionPanelManagerScript.SetAllPipesTransparent ();                
					}
				}
			} else {
				Destroy (this.gameObject);
			}
			_isDragged = false;
		}
	}

}