using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBlasterSeed : MonoBehaviour
{
    public float speed;
    public Vector2 target;

    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target)) >= 0.1f)
        {
            Movement();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
