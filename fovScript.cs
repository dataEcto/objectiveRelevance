using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovScript : MonoBehaviour {
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public bool spotted;

    //The layers
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public float meshResolution; //The number of raycasts per degree

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public List<Transform> snake = new List<Transform>(); //Used for FindVisibleTargets. Actually, The tutorial is using multiple targets. I just dont know how to do one small one

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View the Mesh";
        viewMeshFilter.mesh = viewMesh;
        //Bringing it together to show on the scene
        StartCoroutine("FindTargetsWithDelay", .5f);
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
        //Late update is a thing!
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
                    spotted = true;
                }
                else 
                    spotted = false;
            }
        }

    }

    void DrawFieldOfView()
    {
        float rayCount = Mathf.RoundToInt(viewAngle * meshResolution);  // Stores how many raycast are being made & Rounds up meshResolution 
        float rayAngleSize = viewAngle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>(); //An Array filled with points that will be at the end of the cone, connecting them to create a visual cone
        for (int i = 0; i <= rayCount; i++) //for every ray
        {
            float angle = -transform.eulerAngles.z - viewAngle / 2 + rayAngleSize * i; //Our current angle will be this
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius,Color.red);
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point); //A point at the end of every ray
        }

        int vertexCount = viewPoints.Count + 1; //The vertexes are what makes the triangles, mentioned earlier. The +1 is for the origin point
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero; //Our viewmesh is a child of the guard. All of the pos of the vertices are relative to the guard. we dont use transform.position
        for (int i = 0; i < vertexCount - 1; i++) //The +1 is there because we did stuff with the first vertice
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);  //Again, skipping over the first one here

            if (i < vertexCount - 2)
            {
                triangles[i + 3] = 0; //each triangle starts at the origin
                triangles[i * 3 + 1] = i + 1; //The next point of the triangle
                triangles[i * 3 + 2] = i + 2; //The last point of the triangle
                //tl;dr each time i is 0, itll set the first 3 vertices, when i is 2, the second set, and so on...
            }
        }

        //Code to let some meshes appear on every vertices and triangles
        viewMesh.Clear(); 
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals(); 
       
    }

    ViewCastInfo ViewCast(float globalAngle) 
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }


    //This method will take in the Angle, then return the direction of the angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0); //We don't need Z since we're working in 2D

    }

    public struct ViewCastInfo //A way to store information about the raycast we have
    {
        public bool hit; //Does the raycast hit something?
        public Vector3 point; //Whats the end point of the ray?
        public float dst; //What is the length?
        public float angle; //What is the angle the ray is fired at?

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle) //Constructor (netbeans)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

}

