using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetStraightVelocity();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            //SFX

            //Damage Enemy

            //Destroy bullet
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }
    

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
