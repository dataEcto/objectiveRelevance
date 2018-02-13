using UnityEngine;

public class guardAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    public float timer;

    public bool timerStart;
    public bool speedStop;

    public Ray2D myRay;
    public Transform snakePos;

    public bool test;

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
        //Today we learned about Switch statements
        //This could help
    

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        //(vector a, vector b, speed);
        // this will make the enemy follow Snake around!

        //Check to see if the Guard has reached the patrol point
       
     
        if (Vector3.Distance (transform.position, currentPatrolPoint.position) <= .1f )
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
            speed = 3;
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
        }

        
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
