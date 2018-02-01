using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsOnPlay : MonoBehaviour {

	private Database _database;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		this.transform.GetComponent<SpawnsCreation> ().CreateInitialSpawns ();
		#endif
		_database = GameObject.Find("Database").GetComponent<Database>();
	}

	public void BuildEnemies (Transform enemies) {
		foreach (Transform spawn in this.gameObject.transform) {

			EnemyList list = spawn.GetComponent<EnemyList> ();
			if (list.EnemyPresent ()) {
			
				Enemy enemyToBuild = list.GetNextEnemy ();

				string name = enemyToBuild.GetEnemyType ().ToString ();
				GameObject prefab = _database.GetPrefab ("Enemy", name);

				GameObject enemy = Instantiate (prefab);
				enemy.transform.position = spawn.transform.position;
				enemy.transform.parent = enemies;
			}
		}
	}

	public void ResetSpawns () {
		foreach (Transform spawn in this.gameObject.transform) {
			EnemyList list = spawn.GetComponent<EnemyList> ();

			list.ResetEnemyNum ();
		}
	}
}
