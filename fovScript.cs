using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovScript : MonoBehaviour {
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //The layers
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public float meshResolution; //The number of raycasts per degree

    public List<Transform> snake = new List<Transform>(); //Used for FindVisibleTargets. Actually, The tutorial is using multiple targets. I just dont know how to do one small one

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void Update()
    {
        //DrawFieldOfView();
    }

    //The CoRoutine
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }

    }

    void FindVisibleTargets() //Allow us to do something if the player is in the FOV (disregarding walls of course)
    {
        //Just so the Array doens't get cluttered and avoid duplicates
        snake.Clear();

        //Adds targets in view radius to an array
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        //For every targetsInViewRadius it checks if they are inside the field of view
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //If line draw from object to target is not interrupted by wall, add target to list of visible 
                //targets
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) 
                {
                    snake.Add(target);

                }
            }
        }

    }
    
  /*  void DrawFieldOfView()
    {
        float rayCount = Mathf.RoundToInt(viewAngle * meshResolution);  //Rounds up meshResolution 
        float rayAngleSize = viewAngle / rayCount;

        for (int i = 0; i <= rayCount; i++) //for every ray
        {   
            float angle =  viewAngle * 2 + rayAngleSize * i; //Our current angle will be this
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius,Color.blue);
        }

    }
  */
    //This method will take in the Angle, then return the direction of the angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0); //We don't need Z since we're working in 2D

    }

}

