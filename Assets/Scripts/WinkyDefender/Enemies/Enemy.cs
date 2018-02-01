using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

	private e_EnemyType _enemyType;
	private int 		_enemyLevel;

	// Constructor
	public Enemy (e_EnemyType enemyType, int enemyLevel) {
		_enemyType	= enemyType;
		_enemyLevel = enemyLevel;
	}

	public e_EnemyType GetEnemyType () {
		return (_enemyType);
	}

	public int GetEnemyLevel () {
		return (_enemyLevel);
	}
}
