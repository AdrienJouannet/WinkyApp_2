using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	private GameObject 	_turretBuilt;
	private BuildManager _buildManager;

	// Use this for initialization
	void Start ()
	{
		_buildManager = BuildManager.instance;
	}

	public Vector3 GetBuildPosition ()
	{
		return transform.position;
	}

	public void SetTurretBuilt(GameObject turret)
	{
		_turretBuilt = turret;
	}

	public void OnMouseDown()
	{
		if (this.tag == "UnitSpawn" && !TurretOnTile () && _buildManager.GetTurretToBuild () && _buildManager.GetUnitType().GetUnitLeft() > 0)
			_buildManager.PutTurretOnTile (_buildManager.CreateTurret (_buildManager.GetTurretToBuild ()), this, _buildManager.GetUnitType());
	}

	public bool TurretOnTile()
	{
		if (_turretBuilt)
			return (true);
		else
			return (false);
	}


}
