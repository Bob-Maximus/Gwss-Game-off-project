using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class Player : MonoBehaviour
{
    //getting some variables for speed and jumpheight
    [SerializeField] public float speed;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float acceleration;

    //getting gameobject that will check ground;
    [SerializeField] public GroundCheck groundCheck;

    //getting rigidbody fpr physics stuff
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //getting rigidbody from player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
    }

    //function to move player according to input
    public void InputMovement()
    {
        //creating a variable for storage of our velocities prior to setting our real velocities to them
        Vector2 idealMovement = new Vector2(0, rb.velocity.y);

        //getting player inpput and changing velocities according to them
        if (Input.GetKey(KeyCode.A))
        {
            idealMovement.x = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            idealMovement.x = speed;
        }

        //if player is touching ground and jumping, jump
        if (Input.GetKey(KeyCode.Space) && groundCheck.grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //smoothly turning our real velocities into ideal velocities
        rb.velocity = Vector2.Lerp(idealMovement, rb.velocity, Time.deltaTime * acceleration);
    }
}
