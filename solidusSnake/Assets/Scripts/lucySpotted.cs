using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lucySpotted : MonoBehaviour
{
    public float yPos;
    public float timer = 0;
    public bool timerOn;
    public bool spotted;
    float a = 0;
    float b = 1;
    

    // Use this for initialization
    void Start()
    {
        timerOn = false;
        spotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Guard").GetComponent<fovScript>().spotted == true || spotted)
        {

            GetComponent<ParticleSystem>().Play();

        }
       
    }

}

 
