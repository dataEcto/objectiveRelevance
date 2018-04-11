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
   
        // _myAS.PlayOneShot(clip) - Plays it once, with no reference left behind
        // For keeping references

        // private List<AudioSource> _myASs
        //  GameObject clipGO = new GameObject();
        //Go ahead and make a blank empty game object
        // clipGo.transform.SetParent(this.transform)
        //Makes the clipGo a child of this sound component
        //clipGo.AddComponent<AudioSource>();
        //clipGO.AddComponent<Audiosource>().clip = clip; Which was set up above before start
        //clipGO.AddComponent<Audiosource>().Play;
        //_myASs.Add(clipGO.GetComponent<AudioSource>());

        //This is a bit messy, so we can make it a new class just to clean it up
        //Public class SoundEffect (this is a manager)
        //Public AudioClip clip;
        //public float clipDuration
        //public float internalTimer;
        //
        //public SoundEfeet (AudioClip _clip)
        // internalTimer = 0;
        // clip = _clip;
        // clipDuration = clip.length; checking the length 
        // CreateAndPlay();
        //
        //public void updateTimer()
        // internalTimer += Time.deltaTime; 
        //if (internalTimer >= clipDuration)
        //Delete the game object
        //create a public bool readyForCleanup
        //readyForCleanup = True;

        //Foreach(SoundEffect se in _mySFX)
        // se.updateTimer();
        //if (Security.ReadyForCleanup == true)


        // private void CreateAndPlay()
        //The thing we had above basically, minus the _myASs.Add
        //It will create an audio source, load it, then create it.
        //also add ClipGO = new GameObject(); on top

       //pausing sound
       //private bool _P;
       //privagte bool isPaused;
       //{
       // get
       // return _p;
       //set
       // if value!= _p
       // _p = value;
       // if (_p == true)
        // clipGo.getComponent().play();
        //else
        // clipGo.getComponent().pause();
    }
    }

