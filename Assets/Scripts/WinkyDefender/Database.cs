using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {

	public GameObject[] prefabUnitPanel;
	public GameObject[] prefabUnit;
	public GameObject[] prefabEnemy;
	public GameObject[] prefabSpawn;
	public GameObject[] prefabObstacle;
	public GameObject[] prefabSpawnInfo;
	public Material[]	tileMaterial;

	public const string unitPanel	= "UnitPanel";
	public const string unit		= "Unit";
	public const string enemy		= "Enemy";
	public const string spawn		= "Spawn";
	public const string obstacle	= "Obstacle";
	public const string spawnInfo	= "SpawnInfo";

	private GameObject[] _GetPrefabList (string prefabName){
		switch (prefabName) {
		case unitPanel:
			return (prefabUnitPanel);
		case unit:
			return (prefabUnit);
		case enemy:
			return (prefabEnemy);
		case spawn:
			return (prefabSpawn);
		case obstacle:
			return (prefabObstacle);
		case spawnInfo:
			return (prefabSpawnInfo);
		default:
			break;
		}
		return (null);
	}

	public GameObject GetPrefab (string prefabName, string name)
	{
		foreach (GameObject prefab in _GetPrefabList (prefabName))
		{
			if (name == prefab.name)
				return (prefab);
		}
		return (null);
	}

	public Material GetMaterial (string materialName)
	{
		foreach (Material material in tileMaterial)
		{
			if (material.name == materialName)
				return (material);
		}
		return (null);
	}
}
