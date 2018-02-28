using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour {

    public GameObject Guard;
    public AudioSource _AudioSource1;
    public AudioSource _AudioSource2;
    


    // Use this for initialization
    void Start () {

        _AudioSource1.Play();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Guard.GetComponent<fovScript>().spotted == true)
        {

            if (_AudioSource1.isPlaying)
            {

                _AudioSource1.Stop();

                _AudioSource2.Play();

            }

            


        }
       else if (Guard.GetComponent<fovScript>().spotted == false && _AudioSource2.isPlaying)
        {

            _AudioSource2.Stop();

            _AudioSource1.Play();

        }

    }
    }

