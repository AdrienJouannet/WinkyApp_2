using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelValueDebug : MonoBehaviour
{

	public Accelero ac;
	public Text Input;
	private GameObject player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player");
		ac = player.GetComponent<Accelero>();
	}

	// Update is called once per frame
	void Update()
	{
		if (ac != null)
		{
			string xval = "X :" + (ac.data[0].ToString());
			string yval = "Y :" + (ac.data[1].ToString());

			Input.text = xval + "\n" + yval;
		}
	}
}