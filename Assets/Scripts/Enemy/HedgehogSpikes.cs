using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogSpikes : MonoBehaviour
{
    public float lifetime = 1f;  // Defines how long before the bullet is destroyed
    public float speed = 1f;
    public HedgehogEnemy parent;

    private CapsuleCollider2D collider;
    private Vector2 spawnPoint;
    private float timer = 0f;


    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }
    void Update()
    {
        if (timer >= lifetime) Destroy(gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }
    private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<PlayerHealth>() != null)
        {
            collision.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(parent.Damage());
        }
        
        if(collision.transform.gameObject.layer != this.gameObject.layer)
        {
            Destroy(gameObject);
        }  
    }
}
