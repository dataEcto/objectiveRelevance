using UnityEngine;
using UnityEngine.SceneManagement;

public class guardAI : MonoBehaviour {

    public Transform[] patrolPoints;
    float speed;
    float timer;
    float delay;
   

    bool timerStart;
    bool speedStop;
    

    Animator anim;
    public AudioClip alert;
    public AudioClip walking;
    AudioSource myAudio;
   

    bool alreadyPlayed = false;


    Transform currentPatrolPoint;
    //A way to check the current patrol point our Guard is on
    int currentPatrolIndex;
    //For checking the array

	void Start () {

        timer = 3;
        delay = 1;
       
        delay = 2;
        
        timerStart = false;
        speedStop = false;

        anim = transform.Find("GuardSprite").GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = walking;

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];


        

    }
	
	void Update () {

       
        transform.Translate(Vector3.up * Time.deltaTime * speed);
       if (speed > 0)
        {
            myAudio.PlayOneShot(walking,1);
        }
    
        //Check to see if the enemy is aware of the player - else, just patrol.
        if (GameObject.Find("Guard").GetComponent<fovScript>().spotted == false)
        {
            //The Patrol part
            Patrol();
  
        }

        if ((GameObject.Find("Guard").GetComponent<fovScript>().spotted == true))
        {
           
            timer = 0;
            speed = 0;

            delay -= Time.deltaTime;


            if (!alreadyPlayed && myAudio.clip == walking)

            GameObject.Find("Snake").GetComponent<snakeMovement>().canMove = false;

            if (!alreadyPlayed)

            {
                myAudio.clip = alert;
                myAudio.PlayOneShot(alert, 1);
                alreadyPlayed = true;
            }
            
            if (delay <= 0)
            {
                SceneManager.LoadScene("gameOver");
            }



        }


        if (anim != null)
        {

            if (anim.runtimeAnimatorController != null)
            {
                if (transform.rotation.z >= -180 && transform.rotation.z <= - 170)
                {
                    anim.SetBool("down", true);
                    anim.SetBool("right", false);
                    anim.SetBool("up", false);
                    anim.SetBool("left", false);
                    //anim.Play("patrolDown");

                }

                if (transform.rotation.z < 0 && transform.rotation.z >= -90)
                {
                    anim.SetBool("down", false);
                    anim.SetBool("right", false);
                    anim.SetBool("up", true);
                    anim.SetBool("left", false);
                    //anim.Play("patrolRight");
                }

                if (transform.rotation.z > 0 && transform.rotation.z <= 1) 
                {
                    anim.SetBool("down", false);
                    anim.SetBool("right", false);
                    anim.SetBool("up", true);
                    anim.SetBool("left", false);
                    //anim.Play("patrolUp");
                }

                if (transform.rotation.z >= 89 && transform.rotation.z <= 91)
                {
                    anim.SetBool("down", false);
                    anim.SetBool("right", false);
                    anim.SetBool("up", true);
                    anim.SetBool("left", false);
                    //anim.Play("patrolLeft");
                }
            }

              
        }

       
       
    }

    void Patrol()
    {
        //Check to see if the Guard has reached the patrol point
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) <= .1f)
        {

            speedStop = true;
            timerStart = true;

            //Now that we reached the next point, grab the next one.
            //We also need to check if we have any more patrol Points
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }

           
        }

        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        //Turning to face the next point
        Vector3 patrolPointDirection = currentPatrolPoint.position - transform.position;
        //Gives us radians (converted then to degrees) to change the angle
        float angle = Mathf.Atan2(patrolPointDirection.y, patrolPointDirection.x) * Mathf.Rad2Deg - 90f;
        //Allowing for rotations.
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //Apply this to the transform itself. Time.delta time allows it to rotate slower, avoiding the weird glitchiness of it spazzing out basically
         transform.rotation = Quaternion.RotateTowards(transform.rotation, q , 180 *Time.deltaTime); 

        ///IMPORTANT!! Do not start a guard ON TOP of a patrol point, or else it will start spinning forever! Put it as far away as possible!!



        if (speedStop == true && timer >= 0)
            speed = 0;
        
        else
        {
            speed = 3;
            speedStop = false;
        }


        if (timerStart == true)
        
            timer -= Time.deltaTime;
        

        if (speed > 0)
        {
            timer = 3;
            timerStart = false;
        }

       

    }


    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            
         
            speed = 0;

        }
           
        //So that enemies can pass through each other
        if (collisionInfo.gameObject.tag == "enemy")
            Physics2D.IgnoreCollision(collisionInfo.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
