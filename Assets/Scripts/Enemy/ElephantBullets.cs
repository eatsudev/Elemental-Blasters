using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBullets : MonoBehaviour
{
    public float lifetime = 1f;
    public float speed = 1f;
    public ElephantEnemy parent;
    public LayerMask notDestroyable;

    private Vector2 spawnPoint;
    private float timer = 0f;
    void Update()
    {
        if (timer >= lifetime) Destroy(gameObject);
        timer += Time.deltaTime;

        notDestroyable = parent.notDestroyable;

        Movement();
    }
    private void Movement()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<PlayerHealth>() != null)
        {
            collision.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(parent.Damage());
        }

        if (!((notDestroyable.value & (1 << collision.gameObject.layer)) > 0))
        {
            Destroy(gameObject);
        }
    }
}
