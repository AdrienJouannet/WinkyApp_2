using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float spawnCD = 0.4f;
	public float spawnCDWave = 2;

	private GameObject _enemy;

	// The Serializable attribute lets you embed a class with sub properties in the inspector.
	[System.Serializable]
	public class WaveComponent {
		// Enemy type && number of this enemy
		public GameObject enemyPrefab;
		public int num;
		// don't show this component in the Inspector
		[System.NonSerialized]
		public int spawned = 0;
	}

	// This allow multi waves
	public WaveComponent[] waveComps;

	// Update is called once per frame
	void Update () {
		spawnCDWave -= Time.deltaTime;
		if (spawnCDWave < 0) {
			spawnCDWave = spawnCD;

			bool didSpawn = false;
			foreach (WaveComponent wc in waveComps) {
				if (wc.spawned < wc.num) {
					_enemy = Instantiate (wc.enemyPrefab, this.transform.position, this.transform.rotation);
					_enemy.name = "Enemy";
					_enemy.transform.parent = GameObject.Find ("Enemies").transform;
					wc.spawned++;
					didSpawn = true;
					break;
				}
			}

			if (didSpawn == false) {
				if (this.transform.parent.childCount > 1) {
					this.transform.parent.GetChild (1).gameObject.SetActive (true);
				} else {
					this.transform.gameObject.SetActive (false);
				}
				// Destroy the actual Spawner!
				Destroy (gameObject);
			}
		}
	}
}
