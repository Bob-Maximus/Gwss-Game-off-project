using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winiftouch : MonoBehaviour
{
public GameObject winscreen;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            winscreen.SetActive(true);
        }
    }
}
