using System;
using System.Collections;
using System.Collections.Generic;
using TechTweaking.Bluetooth;
using UnityEngine;
// using System.IO.Ports;


public class Accelero : MonoBehaviour
{

    private float screenLeft;
    private float screenRight;
    private float screenUp;
    private float screenDown;
    public static bool isAlive;

    //SerialPort serial = new SerialPort("COM7", 9600);
    public float[] data = new float [2];

    private BluetoothDevice device;
    private float playerColliderRadius;
    private SphereCollider playerCollider;
    private float asteroidColliderRadius;
    private BoxCollider asteroidCollider;

 //   private Coroutine myCoroutine;
    
    void Start()
    {
        device = BasicDemo.device;
        
        isAlive = true;
        screenLeft = 18f;
        screenRight = 23f;
        screenUp = 2f;
        screenDown = -2f;

    }

    // Update is called once per frame
    void Update()
    {
    //    if (!serial.IsOpen)
    //      serial.Open();
    //    StartCoroutine(Moving());
     //   if (myCoroutine == null)
    //        myCoroutine = StartCoroutine("Moving");
        Move();
    }

    IEnumerator Moving()
    {
        byte[] msg = device.read ();//because we called setEndByte(10)..read will always return a packet excluding the last byte 10.
				
        if (msg != null && msg.Length > 0) 
        {   
            string content = System.Text.ASCIIEncoding.ASCII.GetString (msg);
            string[] values = content.Split(',');
            data[0] = float.Parse(values[0]);
            data[1] = float.Parse(values[1]);
        }
        print("toto");
       yield return new WaitForSeconds(0.5f);
    }
    
    
    
    
    void Move()
    {
        
        
        
        byte[] msg = device.read ();//because we called setEndByte(10)..read will always return a packet excluding the last byte 10.
				
        if (msg != null && msg.Length > 0) 
        {   
            string content = System.Text.ASCIIEncoding.ASCII.GetString (msg);
            string[] values = content.Split(',');
            data[0] = float.Parse(values[0]);
            data[0] = data[0] / 10;
            data[1] = float.Parse(values[1]);
        }

    //    data[0] = (data[0] / 10.0f);
   //     data[1] = data[1] / 10;
    /*    if (data[0] > 180)
            data[0] -= 360;*/
       
        
       transform.position = new Vector3(data[0], data[1], transform.position.z);


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