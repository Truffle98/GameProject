using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D body;
    private Animator anim;
    private bool movingUp;
    private bool movingDown;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        if (horizontalInput > .01f)
        {
            transform.localScale = new Vector3(-6, 6, 6);
        }
        else if (horizontalInput < -.01f)
        {
            transform.localScale = Vector3.one * 6;
        }

        if (verticalInput > .01f)
        {
            movingUp = true;
        }
        else if (verticalInput == 0)
        {
            if (movingUp)
            {
                movingUp = false;
            }
            else if (movingDown)
            {
                movingDown = false;
            }
        }
        else if (verticalInput < -.01f)
        {
            movingDown = true;
        }

        //Set animator parameters
        anim.SetBool("Running", horizontalInput != 0);
        anim.SetBool("UpRunning", movingUp);
    }
}
