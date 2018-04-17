using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class snakeMovement : MonoBehaviour {

    Rigidbody2D snakeBody;
    public float speed;
    Animator anim;
    public bool canMove = true;

    // Use this for initialization
    void Start () {
        snakeBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

       

        if (canMove == true)

        {
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

            //Raw returns the number right away, without any decimals
            //Better for non-gamepad use
            float input_x = Input.GetAxisRaw("Horizontal");
            float input_y = Input.GetAxisRaw("Vertical");


            //Some math to make this easier for a more accurate boolean.
            bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

            anim.SetBool("isWalking", isWalking);
            if (isWalking)
            {
                //Put these values into the animator controller
                anim.SetFloat("x", input_x);
                anim.SetFloat("y", input_y);
            }
        }

        else
        {
            snakeBody.velocity = new Vector2(0, 0);
            speed = 0;
            anim.SetBool("isWalking", false);

        }

        if (Input.GetKey("escape"))
            Application.Quit();

    }


    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "goal")
            SceneManager.LoadScene("win");

        if (collisionInfo.gameObject.tag == "enemy")
        {
                SceneManager.LoadScene("gameOver");
        }
           
        if (collisionInfo.gameObject.tag == "tutorial end")
        {
            SceneManager.LoadScene("introText");
        }

    }

}
