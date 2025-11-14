using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //bool for if player is touching ground
    public bool grounded;

    //if touching object, grounded. If not, ungrounded
    void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
    }
}
