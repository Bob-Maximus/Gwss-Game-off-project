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

    //getting gameobject that will check ground and sides;
    [SerializeField] public GroundCheck groundCheck;
    [SerializeField] public GroundCheck rightGroundCheck;
    [SerializeField] public GroundCheck leftGroundCheck;

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

        //if touching wall, set velocity to 0 as to not just fall
        if (rightGroundCheck.grounded || leftGroundCheck.grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        //if player tries to jump, check if can jump
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //check which ground check is grounded and jump accordingly
            if (groundCheck.grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 3/4);
            }
            else if (rightGroundCheck.grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 3 / 4);
                idealMovement.x = -jumpHeight * 3;
            }
            else if (leftGroundCheck.grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 3 / 4);
                idealMovement.x = jumpHeight * 3;
            }
        }

        //smoothly turning our real velocities into ideal velocities
        float realVelocity = Mathf.Lerp(rb.velocity.x, idealMovement.x, Time.deltaTime * acceleration);
        rb.velocity = new Vector2(realVelocity, rb.velocity.y);
    }
}
