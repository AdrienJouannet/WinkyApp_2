using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TabletAccelero : MonoBehaviour
{

    private float screenLeft;
    private float screenRight;
    private float screenUp;
    private float screenDown;
    public static bool isAlive;

    private float playerColliderRadius;
    private CircleCollider2D playerCollider;
    private float asteroidColliderRadius;
    private BoxCollider2D asteroidCollider;
    private Light playerLight;

    void Start()
    {
        isAlive = true;
        screenLeft = 17f;
        screenRight = 23f;
        screenUp = 2f;
        screenDown = -2f;
        playerLight = GetComponent<Light>();
        playerLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float tmp = Input.acceleration.x;
        float tmp2 = Input.acceleration.y;

       transform.position = new Vector3(transform.position.x + tmp, transform.position.y + tmp2, 0);

//        transform.position = Vector3.Lerp(transform.position, new Vector3(data[0], 0, 0), Time.deltaTime);
        if (transform.position.y < screenDown)
        {
            Vector3 pos = transform.position;
            pos.y = screenDown;
            transform.position = pos;
        }

        if (transform.position.x < screenLeft)
        {
            Vector3 pos = transform.position;
            pos.x = screenLeft;
            transform.position = pos;
        }
        if (transform.position.x > screenRight)
        {
            Vector3 pos = transform.position;
            pos.x = screenRight;
            transform.position = pos;
        }
        if (transform.position.y > screenUp)
        {
            Vector3 pos = transform.position;
            pos.y = screenUp;
            transform.position = pos;
        } 
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (asteroidCollider == null)
        {
            asteroidCollider = col.gameObject.GetComponent<BoxCollider2D>();
            asteroidColliderRadius = asteroidCollider.size.x * col.transform.lossyScale.z;

            Debug.Log(asteroidColliderRadius + playerColliderRadius);
            Debug.Log(col.transform.position.z);
        }
        if (col.transform.position.z >
            asteroidColliderRadius + playerColliderRadius)
        {
            return;
        }
         if (col.gameObject.tag == "Asteroid")
         {
             print("COLLIDER = " + col.gameObject.name);
             isAlive = false;
         }
        if (col.gameObject.tag == "Item")
        {
            playerLight.enabled = true;
            print("BONUS !");
        }

    }
}