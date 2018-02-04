using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    public float timer;
    
    Transform currentPatrolPoint;
    Vector3 guardPosition;
    //A way to check the current patrol point our Guard is on
    int currentPatrolIndex;
    //For checking the array
    bool isMoving;

	// Use this for initialization
	void Start () {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        timer = 10;
        guardPosition = transform.position;
        isMoving = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (isMoving == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        //Check to see if the Guard has reached the patrol point
        if (Vector3.Distance (transform.position, currentPatrolPoint.position) <= .1f )
        {
            //Think backwards: this timer is in the wrong place, 
            //because while the timer does go down, it only does down near a patrol point
            //so we need it to stop at a point and then go down so it can move again
            isMoving = false;

            timer -= 1;

            if (timer < 0)
            {
                timer = 10;
                isMoving = true;
            }
           
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
