using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {


	public KeyCode moveLeft = KeyCode.Q;
	public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.Z;
    public KeyCode moveDown = KeyCode.S;

    public static bool isAlive;

    private float screenLeft;
    private float screenRight;
    private float screenUp;
    private float screenDown;

    private float playerColliderRadius;
    private SphereCollider playerCollider;
    private float asteroidColliderRadius;
    private BoxCollider asteroidCollider;

    
    // Use this for initialization
    void Start () {
        isAlive = true;
        screenLeft = 17.3f;
        screenRight = 22.7f;
        screenUp = 1.3f;
        screenDown = -1.4f;

        playerCollider = GetComponent<SphereCollider>();
        playerColliderRadius = playerCollider.radius;
        
        
    }
    	
    void Move()
    {
        if (Input.GetKey(moveLeft))
        {
            transform.Translate(-Vector2.right * 4f * Time.deltaTime);
            if (transform.position.x < screenLeft)
            {
                Vector3 pos = transform.position;
                pos.x = screenLeft;
                transform.position = pos;
            }
        }
        if (Input.GetKey(moveRight))
        {
            transform.Translate(Vector2.right * 4f * Time.deltaTime);
            if (transform.position.x > screenRight)
            {
                Vector3 pos = transform.position;
                pos.x = screenRight;
                transform.position = pos;
            }
        }
        if (Input.GetKey(moveUp))
        {
            transform.Translate(Vector2.up * 4f * Time.deltaTime);
              if (transform.position.y > screenUp)
            {
                Vector3 pos = transform.position;
                pos.y = screenUp;
                transform.position = pos;
            }
        }

        if (Input.GetKey(moveDown))
        {
            transform.Translate(-Vector2.up * 4f * Time.deltaTime);
            if (transform.position.y < screenDown)
            {
                Vector3 pos = transform.position;
                pos.y = screenDown;
                transform.position = pos;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
    }

    private void OnCollisionStay(Collision col)
    {
        print("collision !!!");
        if (asteroidCollider == null)
        {
            asteroidCollider = col.gameObject.GetComponent<BoxCollider>();
            asteroidColliderRadius = asteroidCollider.size.x * col.transform.lossyScale.z;
            
            Debug.Log(asteroidColliderRadius + playerColliderRadius);
            Debug.Log(col.transform.position.z);
        }
       /* if (col.transform.position.z >
            asteroidColliderRadius + playerColliderRadius)
        {
            return;
        } */
        if (col.gameObject.tag == "Asteroid")
        {
            print("COLLIDER = " + col.gameObject.name);
            isAlive = false;
        }
        if (col.gameObject.tag == "Item")
        {
             print("BONUS !");
        }
    } 
}
