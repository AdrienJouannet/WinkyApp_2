using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameEnded;
    public GameObject gameOverUI;
	// Use this for initialization
	void Start () {
        gameEnded = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameEnded)
            return;
/*
 * UNCOMMENT TO PLAY WITH KEYBOARD
        if (MovePlayer.isAlive == false)
        {
            EndGame();
        } 
  */
		if (MovePlayer.isAlive == false)  //change here when you change the controls (IMU or KEYBOARD)
		{
			EndGame();
		} 
		
		
	}

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
}
