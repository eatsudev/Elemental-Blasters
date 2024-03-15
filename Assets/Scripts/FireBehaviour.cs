using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    [SerializeField] private float normalFireSpeed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysFire;
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private float physicsBulletGravity = 3f;

    private Rigidbody2D rb;

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetRBStats();
        InitializeBulletStats();
    }

    private void FixedUpdate()
    {
        if (bulletType == BulletType.Normal)
        {
            transform.right = rb.velocity;
        }
    }

    private void InitializeBulletStats()
    {
        if (bulletType == BulletType.Normal)
        {
            SetStraightVelocity();
        }
        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysFire.value & (1 << collision.gameObject.layer)) > 0)
        {
            //SFX

            //Damage Enemy

            //Destroy bullet
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalFireSpeed;
    }

    private void SetPhysicsVelocity()
    {
        rb.velocity = transform.right * physicsBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void SetRBStats()
    {
        if (bulletType == BulletType.Normal)
        {
            rb.gravityScale = 0f;
        }
        else if (bulletType == BulletType.Physics)
        {
            rb.gravityScale = physicsBulletGravity;
        }
    }
}
