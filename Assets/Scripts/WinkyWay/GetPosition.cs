using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPosition : MonoBehaviour {

    private Collider2D[] col;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        col = Physics2D.OverlapCircleAll(transform.position, 1.0f);

	}
}
