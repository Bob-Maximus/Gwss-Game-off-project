using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secondstilldeath : MonoBehaviour
{
    public float time = 3;
    public float timer = 0;

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            gameObject.SetActive(false);
        }
    }
}
