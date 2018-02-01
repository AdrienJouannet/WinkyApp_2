using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class SpawnCoordinates : MonoBehaviour {

	[SerializeField]
	private Vector2 	_spawnCoordinates;

	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		if (EditorApplication.isPlaying)
			return;
		_PlaceSpawn ();
	}
	#else
	// Use this for initialization
	void Start () {
		_PlaceSpawn ();
	}
	#endif

	private void _PlaceSpawn () {
		this.transform.position = new Vector3 (_spawnCoordinates.x, 0, _spawnCoordinates.y);
	}
}
