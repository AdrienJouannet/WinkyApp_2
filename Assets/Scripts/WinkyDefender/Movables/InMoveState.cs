using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMoveState : MonoBehaviour {

	private bool _inMove = false;

	public void SetInMove (bool state) {
		_inMove = state;
	}

	public bool GetInMove () {
		return (_inMove);
	}
}
