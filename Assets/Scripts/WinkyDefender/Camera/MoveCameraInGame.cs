using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraInGame : MonoBehaviour {

//	private float _borderThickness = 10f;
	private float _speed = 5f;
	private Vector2 _moveDirection;
	private Vector3 _pos;
	private double _sensivity = 1.5f;
	private bool	_moveAllowed = true;

	public void SetMoveAllowed(bool state)
	{
		_moveAllowed = state;
	}
	
	// Update is called once per frame
	void Update () {
		if (_moveAllowed && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			
			_moveDirection = Input.GetTouch(0).deltaPosition;

			if (_moveDirection.x > _sensivity)
				transform.Translate (Vector3.right * _speed * Time.deltaTime, Space.World);
			else if (_moveDirection.x < -_sensivity)
				transform.Translate (Vector3.left * _speed * Time.deltaTime, Space.World);
			if (_moveDirection.y > _sensivity)
				transform.Translate (Vector3.forward * _speed * Time.deltaTime, Space.World);
			else if (_moveDirection.y < _sensivity)
				transform.Translate (Vector3.back * _speed * Time.deltaTime, Space.World);
			_pos = transform.position;
			_pos.z = transform.position.z;
			_pos.z = Mathf.Clamp (transform.position.z, 0, 14);
			_pos.x = Mathf.Clamp (transform.position.x, 0, 18);
			transform.position = _pos;
		}
	}
}
