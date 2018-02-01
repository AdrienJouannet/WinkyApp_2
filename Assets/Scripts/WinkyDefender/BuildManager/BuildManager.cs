using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
	private GameObject _turretToBuild;
	private PlaceUnitsPanel _unitType;

	void Awake ()
	{
//		Debug.Log (this.name);
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public void SetTurretToBuild (GameObject turret)
	{
		_turretToBuild = turret;
	}

	public void SetUnitType(PlaceUnitsPanel unit)
	{
		_unitType = unit;
	}

	public GameObject GetTurretToBuild ()
	{
		return (_turretToBuild);
	}

	public PlaceUnitsPanel GetUnitType()
	{
		return (_unitType);
	}

	public GameObject CreateTurret(GameObject turret)
	{
		return Instantiate (turret);
	}

	public void PutTurretOnTile(GameObject turret, TileManager tile, PlaceUnitsPanel unit)
	{
		turret.transform.position = tile.GetBuildPosition ();
		turret.name = turret.tag;
		turret.transform.parent = GameObject.Find ("Units").transform;
		tile.SetTurretBuilt(turret);
		unit.SetUnitLeft(unit.GetUnitLeft() - 1);
		unit.gameObject.GetComponent<Unit> ().SetTile (tile.gameObject);
	}

}
