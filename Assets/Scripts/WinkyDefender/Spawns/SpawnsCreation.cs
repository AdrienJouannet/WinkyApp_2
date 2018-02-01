using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class SpawnsCreation : MonoBehaviour {

	private MapEditor _mapEditor;
	private Database _database;

	private int _nbSpawnCreated;
	private bool _toBuild = true;

	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		if (EditorApplication.isPlaying)
			return;
		CreateInitialSpawns ();
	}
	#else
	// Use this for initialization
	void Start () {
		CreateInitialSpawns ();
	}
	#endif

	public void CreateInitialSpawns () {
		_mapEditor = GameObject.Find ("MapEditor").GetComponent<MapEditor> ();
		int nbSpawn = _mapEditor.spawns.Length;
		int spawns = this.transform.childCount;

		if (_nbSpawnCreated != nbSpawn || nbSpawn != spawns || _mapEditor.buildSpawns) {
			_DestroyAllSpawn ();
		} else {
			_CheckPosition ();
		}
		if (_toBuild) {
			_BuildSpawns ();
		}
		_nbSpawnCreated = nbSpawn;
	}

	private void _DestroyAllSpawn () {
		_toBuild = true;
		_mapEditor.buildSpawns = false;
		int nbSpawn = this.transform.childCount;
		for (int i = 0; i < nbSpawn; i++) {
			DestroyImmediate (this.transform.GetChild (0).gameObject);
		}
	}

	// TODO: check the spawn type and destroy if different!
	private void _CheckPosition () {
		Vector2 pos;
		int 	i = 0;
		foreach (Transform spawn in this.transform) {
			pos.x = spawn.position.x;
			pos.y = spawn.position.z;
			if (pos != _mapEditor.spawns[i].position ||
				spawn.name != _mapEditor.spawns[i].name) {
				_DestroyAllSpawn ();
				return ;
			}
			i++;
		}
	}

	private void _BuildSpawns () {
		int nbSpawn = _mapEditor.spawns.Length;
		_database = GameObject.Find("Database").GetComponent<Database>();

		string name;
		Vector2 pos;
		GameObject newSpawn;
		for (int i = 0; i < nbSpawn; i++) {
			Spawns spawn = _mapEditor.spawns [i];
			name = spawn.spawnType.ToString ();
			pos = spawn.position;

			newSpawn = Instantiate (_database.GetPrefab("Spawn", name));
			newSpawn.name = spawn.name;
			newSpawn.transform.SetParent (this.transform);
			newSpawn.transform.position = new Vector3 (pos.x, 0.2f, pos.y);

			newSpawn.GetComponent<EnemyList> ().CreateList (spawn.enemyList);
		}
		_toBuild = false;
	}
}
