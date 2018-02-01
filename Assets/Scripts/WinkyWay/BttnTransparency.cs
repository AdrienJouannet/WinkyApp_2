using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BttnTransparency : MonoBehaviour {

    public Button myButton;
    // Use this for initialization
    void Start()
    {
        myButton.image.color = new Color(0f, 0f, 200f, .1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}