using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class snakeMovement : MonoBehaviour {

    Rigidbody2D snakeBody;
    public float speed;

	// Use this for initialization
	void Start () {
        snakeBody = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {

        snakeBody.velocity = new Vector2(0, 0);
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            snakeBody.velocity += new Vector2(0, speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            snakeBody.velocity += new Vector2(0, -speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            snakeBody.velocity += new Vector2(speed, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            snakeBody.velocity += new Vector2(-speed, 0);
        }

    }


    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Finish")
            SceneManager.LoadScene("win");

    }

}
