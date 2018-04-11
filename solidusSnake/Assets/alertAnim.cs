using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertAnim : MonoBehaviour {


    Animator anim;
    bool playIdle;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        playIdle = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Guard").GetComponent<fovScript>().spotted == true)
        {
            anim.SetBool("shouldPlay", true);
            playIdle = true;

            if (playIdle == true)
            {
                anim.SetBool("stayIdle", true);
            }
        }
           


    }
}
