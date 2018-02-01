using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static float timer;
    public Text scoreText;
	// Use this for initialization
	void Start () {
        timer = 0;	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        string minutes = Mathf.Floor(timer / 59).ToString("00");
        string seconds = (timer % 59).ToString("00");
        scoreText.text = minutes + ":" + seconds;
	}
}
