using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour {

	List<Enemy> enemies = new List<Enemy> ();

	private int	_listSize;
	private int _enemyNum = 0;
	private bool _noMoreEnemies = false;

//	private EnemySpawn[] _enemyList;

	public void CreateList (EnemySpawn[] enemyList) {
//		_enemyList = enemyList;
		for (int enemy = 0; enemy < enemyList.Length; enemy++) {
			for (int nb = 0; nb < enemyList [enemy].nbEnemies; nb++) {
				for (int time = 0; time < enemyList [enemy].timeToSpawn; time++) {
					enemies.Add (new Enemy (e_EnemyType.NoEnemy, 0));
				}
				enemies.Add (new Enemy (enemyList [enemy].enemyType,
										enemyList [enemy].enemyLevel));
			}
		}
		_listSize = enemies.Count;
	}

	public Enemy GetNextEnemy () {
		if (_enemyNum < _listSize) {
			int num = _enemyNum;
			_enemyNum++;
			return (enemies [num]);
		}
		return (null);
	}

	public bool EnemyPresent () {
		if (_enemyNum >= _listSize) {
			_noMoreEnemies = true;
//			Debug.Log (this.transform.name + ": no more enemies.");
			return (false);
		}
		else if (enemies [_enemyNum].GetEnemyType () == e_EnemyType.NoEnemy) {
			_enemyNum++;
//			Debug.Log (this.transform.name + ": I will wait sir.");
			return (false);
		}
//		Debug.Log (this.transform.name + ": an enemy appears.");
		return (true);
	}

	public void ResetEnemyNum () {
		_enemyNum = 0;
		_noMoreEnemies = false;
	}

	public bool GetNoMoreEnemies () {
		return (_noMoreEnemies);
	}
}
