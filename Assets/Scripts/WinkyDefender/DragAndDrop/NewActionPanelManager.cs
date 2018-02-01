using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewActionPanelManager : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private Transform _parentUnitType;
    [SerializeField]
    private GameObject _pipePanel;
	[SerializeField]
	private GameObject _maskPanel;
	private int	_nbActions;


	void Start()
	{
		UnitEditor unitEditorScript = GameObject.Find ("MapEditor").GetComponent<UnitEditor> ();
		_nbActions = unitEditorScript.GetNbAction (_parentUnitType.name, this.transform.parent.name);
		_InitMaskPanel (_nbActions, unitEditorScript.GetNbActionMax (_parentUnitType.name, this.transform.parent.name));
	}

	private void _InitMaskPanel(int nbActions, int nbActionsMax)
	{
		if (_nbActions == 0)
			this.transform.parent.gameObject.SetActive (false);
		else
		{
			for (int i = nbActionsMax - nbActions - 1; i >= 0; i--)
			{
				_maskPanel.transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}

	public void SetNumberOfActions(int nb)
	{
		_nbActions = nb;
	}

	public int GetNumberOfActions()
	{
		return (_nbActions);
	}

    public GameObject CreateTmpAction(Image actionImage)
    {
        GameObject tmpAction = new GameObject ();
        tmpAction.name = "tmp Action";
        Image tmpImage = tmpAction.AddComponent<Image>();
        tmpImage.sprite = actionImage.sprite;
        tmpImage.color = Color.green;
        tmpAction.GetComponent<RectTransform> ().SetParent (this.transform);
		tmpAction.transform.localScale = new Vector3(1, 1, 1);
        return (tmpAction);
    }

    public void SetAllPipesTransparent()
    {
        int children = _pipePanel.transform.childCount;
        for (int i = 0; i < children; i++) {
            ChangePipeAlpha (true, i);
        }
    }

    public void ChangePipeAlpha(bool transparent, int childIndex)
    {
        Color color;
        color = _pipePanel.transform.GetChild (childIndex).GetComponent<Image> ().color;
        if (transparent)
            color.a = 0;
        else
            color.a = 255;
        _pipePanel.transform.GetChild (childIndex).GetComponent<Image> ().color = color;
    }

	public GameObject GetActionToReplace(Transform draggedAction)
	{
//		Collider[] hitColliders = Physics.OverlapBox(draggedAction.position, new Vector3(40, 40, 0));
		Rect rect = draggedAction.GetComponent<RectTransform> ().rect;
		Collider[] hitColliders = Physics.OverlapBox(draggedAction.position, new Vector3(rect.width / 2, rect.height / 2, 0));
		float colXMin;
		float colXMax;
		float colYMin;
		float colYMax;
		float colHalfHeight;
		float colWidth;

		foreach (Collider col in hitColliders)
		{
			//          Ajouter verif si collider tuile ou autre objet qui nest pas dans le panel ??
			colHalfHeight = col.GetComponent<RectTransform> ().rect.height / 2;
			colWidth = col.GetComponent<RectTransform> ().rect.width / 2;
			colXMin = col.gameObject.transform.position.x - colWidth;
			colXMax = col.gameObject.transform.position.x + colWidth;
			colYMin = col.transform.position.y - colHalfHeight;
			colYMax = col.transform.position.y + colHalfHeight;
            if (draggedAction != col.gameObject.transform &&
                draggedAction.position.x > colXMin && draggedAction.position.x < colXMax &&
				draggedAction.position.y > colYMin && draggedAction.position.y < colYMax)
			{
				return col.gameObject;
			}
		}
		return null;
	}


    public GameObject GetActionHit(Transform draggedAction, ref bool placeAfter, ref bool replace)
    {
//      Collider[] hitColliders = Physics.OverlapBox(draggedAction.position, new Vector3(40, 40, 0));
		Rect rect = draggedAction.GetComponent<RectTransform> ().rect;
		Collider[] hitColliders = Physics.OverlapBox(draggedAction.position, new Vector3(rect.width / 2, rect.height / 2, 0));
        float colQuarterWidth;
        float colXMiddlePartMin;
        float colXMiddlePartMax;
        float colYMin;
        float colYMax;
        float colHalfHeight;
        foreach (Collider col in hitColliders)
        {
            //          Ajouter verif si collider tuile ou autre objet qui nest pas dans le panel ??
            colHalfHeight = col.GetComponent<RectTransform> ().rect.height / 2;
            colQuarterWidth = col.GetComponent<RectTransform> ().rect.width / 4;
            colXMiddlePartMin = col.transform.position.x - colQuarterWidth;
            colXMiddlePartMax = col.transform.position.x + colQuarterWidth;
            colYMin = col.transform.position.y - colHalfHeight;
            colYMax = col.transform.position.y + colHalfHeight;

			if (draggedAction != col.gameObject.transform &&
				draggedAction.position.y > colYMin && draggedAction.position.y < colYMax)
			{
				if (draggedAction.position.x < colXMiddlePartMin)
				{
					placeAfter = false;
					return col.gameObject;
				}
				if (draggedAction.position.x > colXMiddlePartMin && draggedAction.position.x < colXMiddlePartMax)
				{
					replace = true;
					return col.gameObject;
				}
				if (draggedAction.position.x > colXMiddlePartMax)
				{
					placeAfter = true;
					return col.gameObject;
				}
			}
        }
        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parentUnitType.GetComponent<Unit> ().SetActiveActionPanel (this.gameObject);//changer couleur du panel actif??
    }
}
