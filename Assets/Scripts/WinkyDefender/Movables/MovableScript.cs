using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class MovableScript : MonoBehaviour {

	private bool _timer = false;
//	private bool _inMove = false;

	private SpawnsOnPlay _spawns;
	private Transform _enemies;
	private Transform _units;
	private ActiveUnit _activeUnit;
	private float _tempo;
	private bool _isPlaying = false;
//	private bool _openMenu = false;

	public int nbMoves;
	public bool loop = false;

	private GameObject _winPanel;

	// Use this for initialization
	void Start () {
		_spawns 	= GameObject.Find ("Spawns").GetComponent<SpawnsOnPlay> ();
		_enemies 	= this.transform.GetChild (0);
		_units 		= this.transform.GetChild (1);
		_activeUnit = _units.GetComponent<ActiveUnit> ();
		_tempo = GameObject.Find ("MapEditor").GetComponent<MapEditor> ().tempo;
		_winPanel = GameObject.Find ("Win");
	}

	public void StopGame()
	{
		StopAllCoroutines ();
		_timer = false;
		_isPlaying = false;
		foreach (Transform unit in _units)
		{
			if (unit.gameObject.activeInHierarchy)
				unit.GetComponent<UnitAction> ().StopMovement();
		}
		_activeUnit.ChangeStateOfCommandPanel(true);
//		_ChangeStateOfAllCommandsPanels (true);
	}

	public bool GetIsPlaying() {
		return _isPlaying;
	}

//	public void SetOpenMenu () {
//		if (_openMenu) {
//			_isPlaying = false;
//			_openMenu = !_openMenu;
//		}
//		else if (_isPlaying == false) {
//			_isPlaying = true;
//			_openMenu = !_openMenu;
//		}
//	}

//	private void  _CreateAllActionList()
//	{
//		GameObject Units = GameObject.Find ("Units");
//		foreach (Transform unit in Units.transform)
//		{
//			ActionList actionListScript = unit.gameObject.GetComponentInChildren<ActionList> ();
//			actionListScript.AddActionsToActionList ();
//		}
//	}


	public void PlayGame () {
		_isPlaying = true;
//		_ChangeStateOfAllCommandsPanels (false);
		_activeUnit.ChangeStateOfCommandPanel(false);
		StartCoroutine (_PlayGameWhenButtonClicked());
	}

	private IEnumerator _PlayGameWhenButtonClicked () {
//		_CreateAllActionList ();
		_spawns.ResetSpawns ();
//		_activeUnit.SetActiveUnit (null);
//		EventSystem.current.currentSelectedGameObject.SetActive(false);
//		yield return (StartCoroutine(GameObject.Find("Main Camera Top View").GetComponent<ChangeView>().ChangeCameraView()));
		_timer = true;
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		if (_timer) {
			_spawns.BuildEnemies (_enemies);
			StartCoroutine (_MoveAndShoot ());
			_timer = false;
		}
	}

	private IEnumerator _MoveAndShoot()
	{
		yield return StartCoroutine (_MoveAllUnit ());
		_UpdateRange ();
		yield return StartCoroutine (_Shoot ());
		_CheckVictory ();
		_timer = true;
	}

	private IEnumerator _MoveAllUnit () {

		yield return new WaitForSeconds (_tempo);
		nbMoves = 0;

		foreach (Transform enemy in _enemies) {
			StartCoroutine(enemy.GetComponent<EnemyPathFinding> ().GetPaths (this));
		}

		foreach (Transform unit in _units) {
			if (unit.gameObject.activeInHierarchy) {
				StartCoroutine (unit.GetComponent<UnitAction> ().NextAction (this));
			}
		}

		while (nbMoves > 0) {
			yield return null;
		}
	}

	// // // // // // / //  //

//	private IEnumerator _MoveAndShoot2 () {
//		_inMove = true;
//		yield return StartCoroutine (_MoveAllUnit2 ());
//		yield return StartCoroutine (_WaitEndMove ());
//		yield return StartCoroutine (_UpdateAndShoot ());
//		if (_enemies.childCount > 0)
//			_timer = true;
//	}
//
//	private IEnumerator _MoveAllUnit2 () {
//		yield return new WaitForSeconds (1f);
//		foreach (Transform enemy in _enemies) {
//			enemy.GetComponent<EnemyPathFinding> ().GetPaths ();
//		}
//
//		foreach (Transform unit in _units) {
//			unit.GetComponent<UnitAction> ().NextAction ();
//		}
//	}

//	private bool _CheckEndMove (Transform movable) {
//		foreach (Transform unit in movable) {
//			if (unit.GetComponent<InMoveState> ().GetInMove () == true) {
//				return (true);
//			}
//		}
//		return (false);
//	}
//
//	private IEnumerator _WaitEndMove () {
//		while (_inMove) {
//			bool inMove;
//
//			inMove = _CheckEndMove (_enemies);
//			if (inMove == false) {
//				inMove = _CheckEndMove (_units);
//				if (inMove == false)
//					_inMove = false;
//			}
//			yield return new WaitForSeconds (0.1f);
//		}
//	}

	private IEnumerator _Shoot () {
		
		foreach (Transform unit in _units) {
			if (unit.gameObject.activeInHierarchy) {
				Unit shooter = unit.GetComponent<Unit> ();
				shooter.UpdateTarget ();
				shooter.Shoot ();
			}
		}
		yield return null;
	}

	private void _CheckVictory () {
		foreach (Transform spawn in _spawns.gameObject.transform) {
			if (spawn.GetComponent<EnemyList> ().GetNoMoreEnemies () == false) {
				return;
			}
		}
		if (_enemies.childCount == 0) {
			_winPanel.transform.GetChild(0).gameObject.SetActive (true);
			if (loop)
				StartCoroutine (_LoopAgain ());
		}
	}

	private IEnumerator _LoopAgain () {
		yield return new WaitForSeconds (2);

		DefenseMode defenseMode = this.transform.GetComponent<DefenseMode> ();
		if (defenseMode)
			defenseMode.RelaunchWave ();
		_winPanel.transform.GetChild(0).gameObject.SetActive (false);
	}

	private void _UpdateRange () {
		foreach (Transform unit in _units) {
			unit.GetComponent<AdjustRange> ().UpdateRange ();
		}
	}
}
