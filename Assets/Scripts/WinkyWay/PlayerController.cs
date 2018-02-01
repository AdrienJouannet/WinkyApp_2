using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By default the player is going straight forward

public class PlayerController : MonoBehaviour
{

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public float horizVel = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, 4);

        if (Input.GetKeyDown(moveLeft))
        {
            horizVel = -2;
            StartCoroutine(stopSlide());
        }

        if (Input.GetKeyDown(moveRight))
        {
            horizVel = 2;
            StartCoroutine(stopSlide());
        }
    }


    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        horizVel = 0;

    }
}
