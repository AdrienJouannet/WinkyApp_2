using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFixedPosition : MonoBehaviour {

    private Quaternion	_defaultRot;
	private Vector3		_defaultPos;


    void Start()
    {
        _defaultRot = transform.rotation;
		_defaultPos = transform.position;
    }

    public void LateUpdate()
    {
		if (transform.parent != null) {
//            transform.position = transform.parent.transform.position;
		}
		transform.position = _defaultPos;
        transform.rotation = _defaultRot;
    }
}
