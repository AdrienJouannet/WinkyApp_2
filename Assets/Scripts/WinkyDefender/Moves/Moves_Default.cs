using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves_Default : MonoBehaviour {

	private Transform 	_unit;
	public float		moveSpeed = 2f;

	private MovablesMoves _movablesMoves;

	private bool _rotate;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private Quaternion _arrivalRotation;
	private Quaternion _initialRotation;

//	Recupere mapSize du script BuildGrid, sert a empecher l'unit de sortir de la grille
	private Vector2 _mapSize;

	private InMoveState _inMoveState;

	private Obstacles[] _obstacles;
	private int _nbObstacle;

	private void Start () {
		_unit = gameObject.transform;
		_movablesMoves = GameObject.Find ("Movables").GetComponent<MovablesMoves> ();
		_mapSize = GameObject.Find ("Grid").GetComponent<BuildGrid> ().GetMapSize ();
		_inMoveState = this.gameObject.GetComponent<InMoveState> ();
		_obstacles = GameObject.Find ("MapEditor").GetComponent<MapEditor> ().obstacles;
		_nbObstacle = _obstacles.Length;
	}

	private void _SetPosition () {
		_unit = gameObject.transform;
		_rotate = false;
		_startPosition = _unit.position;
		_endPosition = _unit.position;

		_arrivalRotation = Quaternion.Euler (0, 0, 0);
		_initialRotation = _unit.rotation;
	}

	public IEnumerator MoveUnit (string action, MovableScript script) {
		if (action == "Wait")
			yield break;
		_SetPosition ();
		if (action == "Forward")
			_MoveForward ();
		else if (action == "Backward")
			_MoveBackward ();
		else if (action == "Left")
			_RotateLeft ();
		else if (action == "Right")
			_RotateRight ();
		if (_rotate)
		{
			if (script) {
				script.nbMoves++;
				_inMoveState.SetInMove (true);
			}
			yield return StartCoroutine (_movablesMoves.RotateFromTo (_unit, _initialRotation, _arrivalRotation, script));
		}
		else if (_IsMovable (_endPosition))
		{
			if (script) {
				script.nbMoves++;
				_inMoveState.SetInMove (true);
			}
			yield return StartCoroutine (_movablesMoves.MoveFromTo (_unit, _startPosition, _endPosition, script));
		}
	}

	public void StopMovement()
	{
		StopAllCoroutines ();
		_movablesMoves.StopMovement ();
	}

	private bool _CheckObstacle (Vector2 pos) {
		for (int i = 0; i < _nbObstacle; i++) {
			if (_obstacles [i].position == pos) {
				return true;
			}
		}
		return false;
	}

	private bool _IsOnWinkyPosition(Vector3 arrivalPosition)
	{
		Transform[] winkyTiles;

		winkyTiles = GameObject.Find ("Grid").GetComponent<BuildGrid> ().GetWinkyTiles ();
		int i = 0;
		foreach (Transform winkyTile in winkyTiles)
		{
			if (arrivalPosition.x == winkyTile.position.x && arrivalPosition.z == winkyTile.position.z)
				return true;
			i++;
		}
		return (false);
	}

	private bool _IsMovable (Vector3 arrivalPosition) {
		Vector2 pos;

		pos.x = arrivalPosition.x;
		pos.y = arrivalPosition.z;

		if (pos.x >= _mapSize.x || pos.x < 0 ||
			pos.y >= _mapSize.y || pos.y < 0) {
			return false;
		}
		else if (_CheckObstacle (pos)) {
			return false;
		}
		else if (_IsOnWinkyPosition(arrivalPosition))
			return (false);
		return true;
	}

	private void _MoveForward () {
		if (_unit.eulerAngles.y == 0)
			_endPosition = _startPosition + new Vector3 (0, 0, 1);
		else if (_unit.eulerAngles.y == 90)
			_endPosition = _startPosition + new Vector3 (1, 0, 0);
		else if (_unit.eulerAngles.y == 180)
			_endPosition = _startPosition + new Vector3 (0, 0, -1);
		else if (_unit.eulerAngles.y == 270)
			_endPosition = _startPosition + new Vector3 (-1, 0, 0);
	}

	private void _MoveBackward () {
		if (_unit.eulerAngles.y == 0)
			_endPosition = _startPosition + new Vector3 (0, 0, -1);
		else if (_unit.eulerAngles.y == 90)
			_endPosition = _startPosition + new Vector3 (-1, 0, 0);
		else if (_unit.eulerAngles.y == 180)
			_endPosition = _startPosition + new Vector3 (0, 0, 1);
		else if (_unit.eulerAngles.y == 270)
			_endPosition = _startPosition + new Vector3 (1, 0, 0);
	}

	private void _RotateLeft () {
		_rotate = true;
		if (_unit.rotation == Quaternion.Euler (0, 0, 0))
			_arrivalRotation = Quaternion.Euler (0, 270, 0);
		else if (_unit.rotation == Quaternion.Euler (0, 270, 0))
			_arrivalRotation = Quaternion.Euler (0, 180, 0);
		else if (_unit.rotation == Quaternion.Euler (0, 180, 0))
			_arrivalRotation = Quaternion.Euler (0, 90, 0);
	}

	private void _RotateRight () {
		_rotate = true;
		if (_unit.rotation == Quaternion.Euler (0, 0, 0))
			_arrivalRotation = Quaternion.Euler (0, 90, 0);
		else if (_unit.rotation == Quaternion.Euler (0, 90, 0))
			_arrivalRotation = Quaternion.Euler (0, 180, 0);
		else if (_unit.rotation == Quaternion.Euler (0, 180, 0))
			_arrivalRotation = Quaternion.Euler (0, 270, 0);
	}
}