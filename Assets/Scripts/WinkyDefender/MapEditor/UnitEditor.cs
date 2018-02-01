using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_ActionType
{
	Forward,
	Left,
	Right,
	Wait,
	F1,
	F2
}

[System.Serializable]
public class MainPanelInfos
{
	[Range (1, 12)]
	public int nbActions;
	[HideInInspector]
	public int nbActionsMax = 12;
}

[System.Serializable]
public class FunctionPanelInfos
{
	public bool isActive;
	[Range (1, 8)]
	public int nbActions;
	[HideInInspector]
	public int nbActionsMax = 8;
}


[System.Serializable]
public class ActionUnitData
{
	public MainPanelInfos Main;
	public FunctionPanelInfos F1;
	public FunctionPanelInfos F2;
	public e_ActionType[] ActionsAvailable;

}

public class UnitEditor : MonoBehaviour
{
	public ActionUnitData Shooter;
	public ActionUnitData Sniper;
	public ActionUnitData Slasher;
	public ActionUnitData Constructor;

    private ActionUnitData _GetUnitType(string unitName)
	{
        if (unitName == "Shooter")
            return (Shooter);
        else if (unitName == "Sniper")
            return (Sniper);
        else if (unitName == "Slasher")
            return (Slasher);
        else if (unitName == "Constructor")
            return (Constructor);
        else
            return (null);
	}

	public e_ActionType[] GetActionsAvailable(string unitName)
	{
		ActionUnitData unitType = _GetUnitType (unitName);

		return (unitType.ActionsAvailable);
	}

	public int GetNbActionMax(string unitName, string panelName)
	{
		ActionUnitData unitType = _GetUnitType (unitName);

		if (panelName == "Prog Panel")
			return (unitType.Main.nbActionsMax);
		else if (panelName == "F1 (panel)")
			return (unitType.F1.nbActionsMax);
		else if (panelName == "F2 (panel)")
			return (unitType.F2.nbActionsMax);
		else
			return (0);
	}

    public int GetNbAction(string unitName, string panelName)
    {
        ActionUnitData unitType = _GetUnitType (unitName);

        if (panelName == "Prog Panel")
            return (unitType.Main.nbActions);
        else if (panelName == "F1 (panel)")
        {
            if (unitType.F1.isActive)
                return (unitType.F1.nbActions);
            else
                return (0);
        }
        else if (panelName == "F2 (panel)")
        {
            if (unitType.F2.isActive)
                return (unitType.F2.nbActions);
            else
                return (0);
        }
        else
            return (0);
    }
}
