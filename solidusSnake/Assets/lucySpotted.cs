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
            /*
            if(timer < 1)
            {
                timerOn
            }
            */
            ///timer += Time.deltaTime / 0.5f;
            GetComponent<ParticleSystem>().Play();
            //yPos = Mathf.Lerp(a, b, timer);
            
            //transform.position = new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z);
        }
        /*
        else
        {
            timer = 0;
        }
        */
    }

}

 
