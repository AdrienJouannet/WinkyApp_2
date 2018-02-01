using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSpawnInfo : MonoBehaviour {

	private ScreenEditor _ScreenEditor;
	private Database _database;

	// Use this for initialization
	void Start () {
		_ScreenEditor = GameObject.Find ("MapEditor").GetComponent<ScreenEditor> ();
		_database = GameObject.Find("Database").GetComponent<Database>();

		GameObject newInfo;
		int nbToCreate = _ScreenEditor.spawnInfo.Length;
		for (int i = 0; i < nbToCreate; i++) {
			SpawnInfo spawnInfo = _ScreenEditor.spawnInfo [i];

			newInfo = Instantiate (_database.GetPrefab("SpawnInfo", "SpawnInfo"));
			newInfo.name = spawnInfo.name;
			newInfo.transform.SetParent (this.transform);

			Vector2 pos = spawnInfo.position;
			newInfo.transform.localPosition = new Vector2 (pos.x, pos.y);

			Transform background = newInfo.transform.GetChild (0);
			Transform enemies = newInfo.transform.GetChild (1);
			ActiveBoxElements (background, spawnInfo.orientation.ToString ());
			ActiveBoxElements (enemies, spawnInfo.enemytype.ToString ());

			enemies.GetChild (4).GetComponent<Text> ().text = "x" + spawnInfo.nbEnemies;

			newInfo.SetActive (false);
		}
	}

	private void ActiveBoxElements (Transform toFind, string name) {
		foreach (Transform elem in toFind) {
			if (elem.name == name) {
				elem.gameObject.SetActive (true);
				break;
			}
		}
		return;
	}
}
