using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildGrid : MonoBehaviour {

	public bool			buildGrid = false;

	public Transform 	tilePrefab;
	public Transform	winkyPrefab;

	[SerializeField]
	private Vector2 	_mapSize = new Vector2(18.0f, 14.0f);

	private Transform[] _winkyTiles = new Transform[4];

//	private Vector2[]	UnitSpawn;
//	private Vector2[]	Path;

	private Vector2[]	_winkyPos;

	void Start () {
		if (tilePrefab != null && buildGrid == true)
			_BuildGrid ();
		_SetWinkyTiles ();
	}
	//18 --> 18 / 2 - 1 = 8
	//19 --> 19 / 2 = 8
	public void _BuildGrid() {

		string holderName = "Generated Map";
		Transform generatedMap = transform.Find (holderName);

		if (generatedMap) {
			DestroyImmediate(generatedMap.gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		int i = 0;
		int indexWinkyTiles = 0;
		for (int x = 0; x < _mapSize.x; x++) {
			for (int y = 0; y < _mapSize.y; y++) {
				Transform newTile = Instantiate (tilePrefab, new Vector3 (x, 0, y), Quaternion.identity) as Transform;
				newTile.parent = mapHolder;
				if ((x == (int)_mapSize.x / 2 - 1 || x == (int)_mapSize.x / 2) && (y == (int)_mapSize.y / 2 - 1 || y == (int)_mapSize.y / 2))
				{
					newTile.tag = "WinkyPosition";
					_winkyTiles [indexWinkyTiles] = newTile;
					i++;
				}
				else
					newTile.tag = _GetTag (x, y);
				newTile.name = "Tile " + i;
				i++;
			}
		}
//		_SetWinky ();
		Debug.Log ("Map Generated.");
		buildGrid = false;
	}

	private void _SetWinky()
	{
		float x = (float)(_mapSize.x / 2.0 - 0.5);
		float y = (float)(_mapSize.y / 2.0 - 0.5);

		GameObject previousWinky = GameObject.Find ("Winky");
		if (previousWinky)
			Destroy (previousWinky);
		Transform winky = Instantiate (winkyPrefab, new Vector3 (x, 0, y), Quaternion.identity) as Transform;
		winky.name = "Winky";
		winky.parent = GameObject.Find ("SceneObjects").transform;
	}

	public Transform[] GetWinkyTiles()
	{
		return (_winkyTiles);
	}

	private void _SetWinkyTiles()
	{
		Transform generatedMap = GameObject.Find ("Generated Map").transform;
		int i = 0;
		foreach (Transform tile in generatedMap)
		{
			if (tile.tag == "WinkyPosition")
			{
				_winkyTiles[i] = tile;
				i++;
				if (i == 4)
					return;
			}
		}
	}

	private string _GetTag (int x, int y) {
//		foreach (Vector2 pos in UnitSpawn) {
//			if (pos.x == x && pos.y == y) {
//				Debug.Log ("unit");
//				return ("UnitSpawn");
//			}
//		}
//		foreach (Vector2 pos in Path) {
//			if (pos.x == x && pos.y == y)
//				return ("Path");
//		}
		return ("Tile");
	}

	public Vector2 GetMapSize () {
		return (_mapSize);
	}
}

