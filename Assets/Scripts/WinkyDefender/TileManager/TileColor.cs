using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is executed in Edit Mode
// Comment the following line when developpment is over (?):
// - using UnityEditor;
// - [ExecuteInEditMode]
// - #if UNITY_EDITOR to #endif

#if UNITY_EDITOR
	using UnityEditor;
	[ExecuteInEditMode]
#endif

public class TileColor : MonoBehaviour {

	private GameObject _units;
	private bool _startInEdit = true;
	private Database database;

	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		if (EditorApplication.isPlaying) {
			if (_startInEdit) {
				_ColorTile ();
				_startInEdit = false;
			}
			return;
		} else if (_startInEdit == false)
			_startInEdit = true;
		_ColorTile ();
	}
	#else
	// Use this for initialization
	void Start () {
		_ColorTile ();
	}
	#endif


	private void _ColorTile () {
		Renderer	rend = this.GetComponent<Renderer> ();
		string		tag = this.gameObject.tag;
		database = GameObject.Find ("Database").GetComponent<Database> ();

		if (tag == "Tile")
			rend.material = database.GetMaterial("Color Tile");
		else if (tag == "Path")
			_SetSprite ();
		else if (tag == "UnitSpawn") {
			rend.material = database.GetMaterial("Color UnitSpawn");

			_units = GameObject.Find ("Units");
			foreach (Transform unit in _units.transform) {
				if (unit.position == this.transform.position) {
					unit.GetComponent<Unit> ().SetTile (this.gameObject);
					break;
				}
			}
		}
		else if (tag == "WinkyPosition")
			_SetWinkyTileSprite();
	}

	private void _SetPositionNumber(int[] neighbourg, Vector3 targetPosition)
	{
		int positionNumber = _GetPositionNumber(this.gameObject.transform.position, targetPosition);
		if (neighbourg [0] == 5)
			neighbourg [0] = positionNumber;
		else if (neighbourg [1] == 5)
			neighbourg [1] = positionNumber;
		else if (neighbourg [2] == 5)
			neighbourg [2] = positionNumber;
		else if (neighbourg [3] == 5)
			neighbourg [3] = positionNumber;
	}

	private int _GetPositionNumber(Vector3 currentPosition, Vector3 targetPosition)
	{
		if (targetPosition.x < currentPosition.x)
			return 4;
		else if (targetPosition.x > currentPosition.x)
			return 2;
		else
		{
			if (targetPosition.z > currentPosition.z)
				return 8;
			else
				return 1;
		}
	}

	private void _GetNeighbourg(int[] neighbourg)
	{
		AdjacentTiles.SearchForNeighbourgTiles(this.gameObject); //récupere la liste des cases adjacentes
		foreach (GameObject target in AdjacentTiles.PossibleMoves)
		{
			if ((target.name != this.gameObject.name) && ((target.transform.position.x == transform.position.x) || (target.transform.position.z == transform.position.z)))
				_SetPositionNumber (neighbourg, target.transform.position);
		}
		AdjacentTiles.PossibleMoves.Clear ();
	}

	private void _GetNeighbourgWinky(int[] neighbourg, ref int neighbourgRelativePos)
	{
		AdjacentTiles.SearchForNeighbourgTiles(this.gameObject); //récupere la liste des cases adjacentes
		foreach (GameObject target in AdjacentTiles.PossibleMoves)
		{
			if ((target.name != this.gameObject.name) && ((target.transform.position.x == transform.position.x) || (target.transform.position.z == transform.position.z)))
			{
				_SetPositionNumber (neighbourg, target.transform.position);
				if (target.tag == "Path") {
					neighbourgRelativePos += _GetPositionNumber (this.gameObject.transform.position, target.transform.position);
				}
			}
		}
		AdjacentTiles.PossibleMoves.Clear ();
	}

	private void _SetWinkyTileSprite()
	{
		int[] neighbourg = new int [4];
		int neighbourgRelativePos = 0;
		for (int i = 0; i < neighbourg.Length; i++)
			neighbourg[i] = 5;
		_GetNeighbourgWinky (neighbourg, ref neighbourgRelativePos);
		int total = 0;
		foreach (int current in neighbourg)
		{
			if (current != 5)
				total += current;
		}
		Renderer    rend = this.GetComponent<Renderer> ();

		if (total == 3)
			rend.material = database.GetMaterial("Color Tile WC270");
		else if (total == 5)
			rend.material = database.GetMaterial("Color Tile WC");
		else if (total == 10)
			rend.material = database.GetMaterial("Color Tile WC180");
		else if (total == 12)
			rend.material = database.GetMaterial("Color Tile WC90");
		else if (total == 7)
		{
			if (neighbourgRelativePos == 2)
				rend.material = database.GetMaterial("Color Tile WB270");
			else
				rend.material = database.GetMaterial("Color Tile WP270");
		}
		else if (total == 11)
		{
			if (neighbourgRelativePos == 1)
				rend.material = database.GetMaterial("Color Tile WP180");
			else
				rend.material = database.GetMaterial("Color Tile WB180");
		}
		else if (total == 13)
		{
			if (neighbourgRelativePos == 1)
				rend.material = database.GetMaterial("Color Tile WB");
			else
				rend.material = database.GetMaterial("Color Tile WP");
		}
		else if (total == 14)
		{
			if (neighbourgRelativePos == 2)
				rend.material = database.GetMaterial("Color Tile WP90");
			else
				rend.material = database.GetMaterial("Color Tile WB90");
		}
		else if (total == 15)
		{
			if (neighbourgRelativePos == 3)
				rend.material = database.GetMaterial("Color Tile WX90");
			else if (neighbourgRelativePos == 5)
				rend.material = database.GetMaterial("Color Tile WX180");
			else if (neighbourgRelativePos == 10)
				rend.material = database.GetMaterial("Color Tile WX");
			else
				rend.material = database.GetMaterial("Color Tile WX270");
		}
		else
			rend.material = database.GetMaterial("Color Tile WB");
	}

	private void _SetSprite()
	{
		int[] neighbourg = new int [4];
		for (int i = 0; i < neighbourg.Length; i++)
			neighbourg[i] = 5;
		_GetNeighbourg (neighbourg);
		int total = 0;
		foreach (int current in neighbourg)
		{
			if (current != 5)
				total += current;
		}
		Renderer    rend = this.GetComponent<Renderer> ();
		if (total == 1 || total == 8 || total == 9)
			rend.material = database.GetMaterial("Color Tile I");
		else if (total == 2 || total == 4 || total == 6)
			rend.material = database.GetMaterial("Color Tile I90");
		else if (total == 3)
			rend.material = database.GetMaterial("Color Tile L270");
		else if (total == 5)
			rend.material = database.GetMaterial("Color Tile L");
		else if (total == 10)
			rend.material = database.GetMaterial("Color Tile L180");
		else if (total == 12)
			rend.material = database.GetMaterial("Color Tile L90");
		else if (total == 7)
			rend.material = database.GetMaterial("Color Tile T180");
		else if (total == 11)
			rend.material = database.GetMaterial("Color Tile T270");
		else if (total == 13)
			rend.material = database.GetMaterial("Color Tile T90");
		else if (total == 14)
			rend.material = database.GetMaterial("Color Tile T");
		else if (total == 15)
			rend.material = database.GetMaterial("Color Tile X");
		else
			rend.material = database.GetMaterial("Color Tile X");
	}
}
