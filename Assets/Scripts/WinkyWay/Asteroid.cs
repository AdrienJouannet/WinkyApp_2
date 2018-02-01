using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Vector3 toScale; 
    private WaveSpawner spawn;
//    private int targetColIndex;
    Vector3 target; // going straight to the right zone
    BoxCollider asteroidCol;
    public float speed;

    // Start code should be in Awake but because of a bug of Unity2017 I have to use Start instead 
    private void Start()
    {
        asteroidCol = GetComponent<BoxCollider>(); //disabled at start
        asteroidCol.enabled = false;
        spawn = GameObject.Find("SpawnerManager").GetComponent<WaveSpawner>();
//        targetColIndex = spawn.originSpawner;
        target = new Vector3(transform.position.x, transform.position.y, -3);
        print("speed  = " + speed);
        StartCoroutine(MoveThisObject(target, speed));
    }

    IEnumerator MoveThisObject(Vector3 target, float moveDuration)
    {
        // store the starting position of the object this script is attached to as well as the target position
        Vector3 oldPos = transform.position;
        Vector3 newPos = target;
        float moveTime = 0.0f;

        while (moveTime < moveDuration)
        {
            moveTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, toScale, moveTime / (moveDuration - 0.5f));
            transform.position = Vector3.Lerp(oldPos, newPos, moveTime / moveDuration);
            if ((moveTime + 0.5) >= moveDuration)
                asteroidCol.enabled = true;
            yield return null;
        }
        Destroy(gameObject);
    }

    //Check if Winky is in the same zone as the asteroid
    // if so, winky is hit = game over
    /*
    void HasTouchedWinky()
    {



    }
    */

}
