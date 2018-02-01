using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_InfoOrientation
{
	NW,
	NE,
	SE,
	SW
}

[System.Serializable]
public class SpawnInfo
{
	public string name;
	public Vector2 position;
	public e_InfoOrientation orientation;
	public e_EnemyType enemytype;
	public int nbEnemies;
}

public class ScreenEditor : MonoBehaviour {

	public SpawnInfo[] spawnInfo;
}
