using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    //get camera position
    [SerializeField] private Transform camPos;

    //get chosen acceleration
    [SerializeField] private float acceleration;

    //runs every frame
    private void Update()
    {
        //makes the ideal and alligns it with the players position
        Vector3 idealcamPos = new Vector3(transform.position.x, transform.position.y, camPos.transform.position.z);

        //smooths the transition between the camera position and the ideal position using the acceleration for speed
        camPos.transform.position = Vector3.Lerp(camPos.transform.position, idealcamPos, Time.deltaTime * acceleration);
    }
}
