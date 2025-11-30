using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsGoBy : MonoBehaviour
{
    public float speed;
    public float frequency;
    public bool messUpScale;

    public List<GameObject> things;

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frequency)
        {          
            GameObject obj = Instantiate(things[Random.Range(0, things.Count)]);
            obj.GetComponent<Rigidbody2D>().velocityX = -speed;
            if (messUpScale) obj.transform.localScale = obj.transform.localScale * Random.Range(0.8f, 1.2f);

            timer = 0;
        }
    }
}
