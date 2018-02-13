using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(fovScript))] //Allow us to visual the fov cone. Accesible via the Scene Editor
public class FieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {
        fovScript fow = (fovScript)target;

        //Draws view reach
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);

        //Draws cone of view
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        //A test to see if it truly sees the player
        Handles.color = Color.red;
        foreach (Transform snake in fow.snake)
        {
            Handles.DrawLine(fow.transform.position, snake.position);
        }
    }
}