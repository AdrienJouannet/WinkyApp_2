using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class BuildObstacles : MonoBehaviour {

	private MapEditor _mapEditor;
	private Database _database;

	private bool _toBuild = true;

	private int _nbToBuild;
	private int _nbChild;

	private Vector2 _posDiff;

	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		if (EditorApplication.isPlaying)
			return;
		_Create ();
	}
	#else
	// Use this for initialization
	void Start () {
		_Create ();
	}
	#endif

	private void _Create () {
		_mapEditor = GameObject.Find ("MapEditor").GetComponent<MapEditor> ();
		_nbToBuild = _mapEditor.obstacles.Length;
		_nbChild = this.transform.childCount;

		if (_nbToBuild != _nbChild) {
			_DestroyAll ();
		} else {
			_CheckPosition ();
		}
		if (_toBuild) {
			_Build ();
		}
	}

	private void _DestroyAll () {
		_toBuild = true;
		for (int i = 0; i < _nbChild; i++) {
//			Debug.Log ("DestroyObstacle");
			DestroyImmediate (this.transform.GetChild (0).gameObject);
		}
	}

	private void _CheckPosition () {
		Vector2 pos;
		int 	i = 0;
		foreach (Transform go in this.transform) {
			pos.x = go.position.x - _posDiff.x;
			pos.y = go.position.z - _posDiff.y;
//			Debug.Log ("pos: " + pos);
//			Debug.Log ("posDif: " + _posDiff);
			if (pos != _mapEditor.obstacles[i].position ||
				go.name != _mapEditor.obstacles [i].obstacleType.ToString()) {
				_DestroyAll ();
				return ;
			}
			i++;
		}
	}

	private void _Build () {
		_database = GameObject.Find("Database").GetComponent<Database>();

		GameObject newObstacle;
		Obstacles obstacle;
		Vector2 pos;
		string name;

//		Debug.Log ("build: " + _nbToBuild);
		for (int i = 0; i < _nbToBuild; i++) {
			obstacle = _mapEditor.obstacles [i];
			name = obstacle.obstacleType.ToString ();
			pos = obstacle.position;

			newObstacle = Instantiate (_database.GetPrefab ("Obstacle", name));
			_posDiff.x = newObstacle.transform.position.x;
			_posDiff.y = newObstacle.transform.position.z;
//			Debug.Log ("Here" + newObstacle.transform.position);
			newObstacle.name = name;
			newObstacle.transform.parent = this.transform;
			newObstacle.transform.position = new Vector3 (
												pos.x + newObstacle.transform.position.x,
												newObstacle.transform.position.y,
												pos.y + newObstacle.transform.position.z);
		}

		_toBuild = false;
	}
}
