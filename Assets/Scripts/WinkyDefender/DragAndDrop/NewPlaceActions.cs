using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPlaceActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

    private GameObject _newAction;
    private GameObject _tmpAction;
    private GameObject _actionHit;
    private NewActionPanelManager _actionPanelManagerScript;//a renommer le type
    private GameObject _actionPanel;
    private Unit _unitParentScript;
    private bool _isDragged;
	private bool _replace;
	private bool _placeAfter;
	private GameObject _tutoCanvas;
	private DisplayTutorial _tutoCanvasScript;
	private Vector3 _tmpPos;
	private MovableScript _movableScript;

    void Start()
    {
        _unitParentScript = GetComponentInParent<Unit> ();
        _isDragged = false;
		_replace = false;
		_placeAfter = false;
		_tutoCanvas = GameObject.Find ("Tuto (canvas)");
		_movableScript = GameObject.Find ("Movables").GetComponent<MovableScript> ();
		if (_tutoCanvas)
			_tutoCanvasScript = _tutoCanvas.GetComponent<DisplayTutorial> ();
    }

    private void _CreateNewActionItem()
    {
        _newAction = new GameObject(); //Create the GameObject
        Image NewImage = _newAction.AddComponent<Image>(); //Add the Image Component script
        NewImage.sprite = this.gameObject.GetComponent<Image>().sprite; //Set the Sprite of the Image Component on the new GameObject
		_newAction.GetComponent<RectTransform>().SetParent(this.transform.parent.transform.parent); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        _newAction.transform.localScale = new Vector3(1, 1, 1); //Set the scale to (1, 1, 1) to fit on different resolutions
        _newAction.name = this.name;
        _newAction.tag = "ActionPanel";//pas necessaire maintenant
    }

    private void _AddColliderToActionItem(Transform parent)
    {
        _newAction.GetComponent<RectTransform> ().SetParent (parent); //Assign the newly created Image GameObject as a Child of the Parent Panel.
		Rect rect = _newAction.GetComponent<RectTransform> ().rect;
		_newAction.AddComponent<BoxCollider>();
        _newAction.GetComponent<BoxCollider> ().size = new Vector3 (rect.height, rect.width, 0);//add collider to use OverlapBox
    }

    public void OnPointerClick(PointerEventData eventData)
    {
		if (_tutoCanvasScript)
			_tutoCanvasScript.DisplayNextTutorial ("Action");
		if (!_isDragged && !_movableScript.GetIsPlaying())
        {
            _actionPanel = _unitParentScript.GetActiveActionPanel ();
            _actionPanelManagerScript = _actionPanel.GetComponent<NewActionPanelManager> ();
            if (_actionPanelManagerScript.GetNumberOfActions () > 0) {
                _CreateNewActionItem ();
                _AddColliderToActionItem (_actionPanel.transform);
                _newAction.AddComponent<NewManageActions> ();// a mettre dans add collider?
                _actionPanelManagerScript.SetNumberOfActions (_actionPanelManagerScript.GetNumberOfActions () - 1);
            }
        }
    }

    public GameObject GetDropActionPanel(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult> ();
		eventData.position = _newAction.transform.position;
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
			_CreateNewActionItem ();
			Rect rect = this.GetComponent<RectTransform> ().rect;
			_newAction.GetComponent<RectTransform> ().sizeDelta = new Vector2 (rect.width - 10, rect.height - 10);
			_isDragged = true;
		}
    }


    public void OnDrag(PointerEventData eventData)
    {
		if (!_movableScript.GetIsPlaying ())
		{
			_newAction.transform.position = Input.mousePosition;
			_tmpPos = _newAction.transform.position;
			_tmpPos.y += _newAction.GetComponent<RectTransform> ().rect.height - 10;
			_newAction.transform.position = _tmpPos;
			if (_actionPanel = GetDropActionPanel (eventData))
			{
				_actionPanelManagerScript = _actionPanel.GetComponent<NewActionPanelManager> ();
				if (_actionPanelManagerScript.GetNumberOfActions () > 0)
				{
					_replace = false;
					_actionPanelManagerScript.SetAllPipesTransparent ();
					if (_actionHit)
						_actionHit.GetComponent<Image> ().color = Color.white;
					if (_actionHit = _actionPanelManagerScript.GetActionHit (_newAction.transform, ref _placeAfter, ref _replace))
					{
						if (_replace)
						{
							_actionHit.GetComponent<Image> ().color = Color.red;
							if (_tmpAction)
								Destroy (_tmpAction);
						}
						else if (!_placeAfter)
						{
							_actionPanelManagerScript.ChangePipeAlpha (false, _actionHit.transform.GetSiblingIndex ());
							if (_tmpAction)
								Destroy (_tmpAction);
						}
						else
						{

							int lastChildIndex;
							if (_tmpAction)
								lastChildIndex = _actionPanel.transform.childCount - 2;
							else
								lastChildIndex = _actionPanel.transform.childCount - 1;
							if (_actionHit.transform != _actionPanel.transform.GetChild (lastChildIndex))
							{
								_actionPanelManagerScript.ChangePipeAlpha (false, _actionHit.transform.GetSiblingIndex () + 1);
								if (_tmpAction)
									Destroy (_tmpAction);
							}
							else if (!_tmpAction)
								_tmpAction = _actionPanelManagerScript.CreateTmpAction (this.GetComponent<Image> ());
						}
					}
					else
					{
						if (!_tmpAction)
							_tmpAction = _actionPanelManagerScript.CreateTmpAction (this.GetComponent<Image> ());
					}
         
				}
				else
				{
					_replace = true;
					if (_actionHit)
						_actionHit.GetComponent<Image> ().color = Color.white;
					if (_actionHit = _actionPanelManagerScript.GetActionToReplace (_newAction.transform))
					{
						_actionHit.GetComponent<Image> ().color = Color.red;
					}
				}
			}
			else
			{
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
		if (!_movableScript.GetIsPlaying ())
		{
			if (_actionPanel)
			{
				_AddColliderToActionItem (_actionPanel.transform);
				_newAction.AddComponent<NewManageActions> ();// a mettre dans add collider?
				_unitParentScript.SetActiveActionPanel (_actionPanel);
				if (_replace)
				{
					if (_actionHit)
					{
						_actionHit.GetComponent<Image> ().sprite = _newAction.GetComponent<Image> ().sprite;
						_actionHit.GetComponent<Image> ().color = Color.white;
						_actionHit.name = _newAction.name;
					}
					Destroy (_newAction);
				}
				else
				{
					_actionPanelManagerScript.SetNumberOfActions (_actionPanelManagerScript.GetNumberOfActions () - 1);
					if (_tmpAction)
					{
						_newAction.transform.SetSiblingIndex (_tmpAction.transform.GetSiblingIndex ());
						Destroy (_tmpAction);
						if (_tutoCanvasScript)
							_tutoCanvasScript.DisplayNextTutorial ("Action");
					}
					else
					{
						if (!_placeAfter)
							_newAction.transform.SetSiblingIndex (_actionHit.transform.GetSiblingIndex ());
						else
							_newAction.transform.SetSiblingIndex (_actionHit.transform.GetSiblingIndex () + 1);
						_actionPanelManagerScript.SetAllPipesTransparent ();                
					}
				}
			}
			else
			{
				Destroy (_newAction);
			}
			_isDragged = false;
		}
    }

}

