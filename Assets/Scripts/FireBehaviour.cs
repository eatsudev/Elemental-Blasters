using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    [SerializeField] private float normalFireSpeed = 15f;
    [SerializeField] private int normalFireDamage = 1;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysFire;
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private int physicsFireDamage = 2;
    [SerializeField] private float physicsBulletGravity = 3f;

    private Rigidbody2D rb;
    private int damage;

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
            damage = normalFireDamage;
        }
        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage = physicsFireDamage;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysFire.value & (1 << collision.gameObject.layer)) > 0)
        {
            //SFX

            //Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.TakeDamage((int)damage);
            }

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
