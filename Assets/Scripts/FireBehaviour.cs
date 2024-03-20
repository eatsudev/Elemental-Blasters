using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireBehaviour : MonoBehaviour
{
    [SerializeField] private float normalFireSpeed = 15f;
    [SerializeField] private int normalFireDamage = 1;
    [SerializeField] private float destroyTime = 10f;
    [SerializeField] private LayerMask whatDestroysFire;
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private int physicsFireDamage = 2;
    [SerializeField] private float physicsBulletGravity = 3f;
    [SerializeField] private AudioSource fireTriggerSFX;

    private Rigidbody2D rb;
    private int damage;

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType;
    private float explosionRadius;

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
            //sfx
            fireTriggerSFX.Play();

            //Explosion Animation
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Explode");
            }
            rb.velocity = Vector2.zero;
            // Wait for the duration of the explosion animation
            StartCoroutine(DestroyAfterDelay(10f));

            //Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.TakeDamage((int)damage);
                Debug.Log(damage);
            }
            Debug.Log("Hit something");

        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy bullet
        Destroy(gameObject);
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
