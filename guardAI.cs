using UnityEngine;
using UnityEngine.SceneManagement;

public class guardAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    public float timer;

    bool timerStart;
    bool speedStop;
    public bool musicPlay;

    public Transform snakePos;
    public float awarenessRange;
    public float distanceToTarget;

    Transform currentPatrolPoint;
    //A way to check the current patrol point our Guard is on
    int currentPatrolIndex;
    //For checking the array
 

	// Use this for initialization
	void Start () {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        timer = 10;
        timerStart = false;
        speedStop = false;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.up * Time.deltaTime * speed);
        //Check the distance to the player
      distanceToTarget = Vector3.Distance(transform.position, snakePos.position);
        //Check to see if the enemy is aware of the player - else, just patrol.
        if (gameObject.GetComponent<fovScript>().spotted == false)
        {
            //The Patrol part
            Patrol();
            musicPlay = false;
        }

        if (gameObject.GetComponent<fovScript>().spotted == true)
        {
            timer = 0;
            speed = 3;
            Chase();
            musicPlay = true;
           
        }
        /*
         * 
         *  if (distanceToTarget > awarenessRange)
            {
                Patrol();
                musicPlay = false;
            }
        */

        
       
        
        
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

            currentPatrolPoint = patrolPoints[currentPatrolIndex];

        }



        if (speedStop == true && timer >= 0)
        {
            speed = 0;
        }

        else
        {
            speed = 5;
            speedStop = false;

        }


        if (timerStart == true)
        {
            timer -= Time.deltaTime;

        }

        if (speed > 0)
        {
            timer = 3;
            timerStart = false;
            //Turning to face the next point
            Vector3 patrolPointDirection = currentPatrolPoint.position - transform.position;
            //Gives us radians (converted then to degrees) to change the angle
            float angle = Mathf.Atan2(patrolPointDirection.y, patrolPointDirection.x) * Mathf.Rad2Deg - 90f;
            //Allowing for rotations.
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //Apply this to the transform itself
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        }




    }

    void Chase()
    {
        //Chasing Player AI
        //Could potentially use the fovScript for some parts instead of doing all this
        //Chase the Player! - Turn and Move towards the target
        Vector3 snakeDir = snakePos.position - transform.position;
        float newAngle = Mathf.Atan2(snakeDir.y, snakeDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q2 = Quaternion.AngleAxis(newAngle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q2, 180);
        //GET IT MOVING!
        transform.Translate(Vector3.up * Time.deltaTime * speed);


       

    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
            SceneManager.LoadScene("gameOver");

    }


}
