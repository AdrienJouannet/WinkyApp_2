using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum e_UnitType
{
	Shooter,
	Sniper,
	Slasher,
	Constructor
}

public enum e_EnemyType
{
	Hollow,
	Urchin,
	Dog,
	Heavy,
	NoEnemy
}

public enum e_SpawnType
{
	Spawn
}

public enum e_ObstacleType
{
	Obstacle
}

public enum e_Direction
{
	top,
	left,
	right,
	down
}

[System.Serializable]
public class UnitData
{
	public e_UnitType unitType;
	public Vector2 position;
	public e_Direction orientation;
}

[System.Serializable]
public class UnitDataPanel
{
	public e_UnitType unitType;
	[Range (1, 99)]
	public int nbUnits;
}

[System.Serializable]
public class Spawns
{
	public string name;
	public Vector2 position;
	public e_Direction boxPosition;
	public e_SpawnType spawnType;
	public EnemySpawn[] enemyList;
}
	
[System.Serializable]
public class EnemySpawn {
	public e_EnemyType enemyType;
	[Range (1, 100)]
	public int nbEnemies;
	[Range (1, 100)]
	public int enemyLevel;
	[Range (0, 100)]
	public int timeToSpawn;
}

[System.Serializable]
public class Obstacles
{
	public Vector2 position;
	public e_ObstacleType obstacleType;
}

public class MapEditor : MonoBehaviour {

	public Vector2 mapSize = new Vector2(18f, 14f);

	[Range (0f, 3f)]
	public float	tempo;
	public UnitDataPanel[] unitsInPanel;
	public UnitData[] units;
	public Obstacles[] obstacles;
	public bool buildSpawns;
	public Spawns[] spawns;

	public int GetNbUnitsOfOneUnit(string unitName)
	{
		foreach (UnitDataPanel unit in unitsInPanel)
		{
			if (unit.unitType.ToString () == unitName)
				return unit.nbUnits;
		}
		return (0);
	}
}
