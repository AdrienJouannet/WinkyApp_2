using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustRange : MonoBehaviour {

	public float rangeMax = 4.0f;
	public GameObject rangeList;

	private MapEditor _mapEditor;
	private Transform _obstacles;
	private Transform[] _winkyTiles;

	private int _actualRange;

	void Start () {
		_actualRange = (int)rangeMax;
		_mapEditor	= GameObject.Find ("MapEditor").GetComponent<MapEditor> ();
		_obstacles	= GameObject.Find ("Obstacles").transform;
		_winkyTiles = GameObject.Find ("Grid").GetComponent<BuildGrid> ().GetWinkyTiles ();
	}

	public void UpdateRange () {
		ResetRange ();
		_CheckGrid ();
		_CheckObstacles ();
	}

	public void ResetRange () {
		_ChangeRange ((int) rangeMax);
	}

	private void _ChangeRange (int distance) {
		int i = 0;
		foreach (Transform rangePlane in rangeList.transform) {
			if (i < distance) {
				rangePlane.gameObject.SetActive (true);
			}
			else {
				rangePlane.gameObject.SetActive (false);
			}
			i++;
		}
		_actualRange = distance;
//		Debug.Log (_actualRange);
	}

	private void _CheckGrid () {
		Transform unit = this.transform;
		float distToObstacle = Mathf.Infinity;

		if (unit.eulerAngles.y == 0) {
			distToObstacle = _mapEditor.mapSize.y - unit.position.z - 1;
		}
		else if (unit.eulerAngles.y == 90) {
			distToObstacle = _mapEditor.mapSize.x - unit.position.x - 1;
		}
		else if (unit.eulerAngles.y == 180) {
			distToObstacle = unit.position.z;
		}
		else if (unit.eulerAngles.y == 270) {
			distToObstacle = unit.position.x;
		}
		if (distToObstacle < _actualRange) {
//			Debug.Log ("grid at : " + distToObstacle);
			_ChangeRange ((int)distToObstacle);
		}
//		else
//			_ChangeRange ((int) rangeMax);
	}

	private void _CheckObstacles () {
		Transform unit = this.transform;
		float shortestDistance = Mathf.Infinity;
		float distToObstacle;
		float angle;
		bool changeRange = false;

		foreach (Transform obstacle in _obstacles) {
			distToObstacle = Vector3.Distance (unit.position, obstacle.position);
			angle = Vector3.Angle (unit.forward, obstacle.position - unit.position);
//			Debug.Log ("angle: " + angle);
			if ((distToObstacle < shortestDistance) && angle < 13) {
				shortestDistance = distToObstacle - 1;
//				Debug.Log ("Obstacle!");
				changeRange = true;
			}
		}

		foreach (Transform winkyTile in _winkyTiles)
		{
			distToObstacle = Vector3.Distance (unit.position, winkyTile.position);
			angle = Vector3.Angle (unit.forward, winkyTile.position - unit.position);
			if ((distToObstacle <= shortestDistance) && angle < 10) {
				shortestDistance = distToObstacle - 1;
//				Debug.Log ("Winky!");
				changeRange = true;
			}
		}

		if (changeRange && (int)shortestDistance < _actualRange) {
			_ChangeRange ((int)shortestDistance);
		}
	}
}
