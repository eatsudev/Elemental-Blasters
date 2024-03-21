using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : MonoBehaviour
{
    [SerializeField] private float normalWindSpeed = 15f;
    [SerializeField] private int normalWindDamage = 1;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysWind;
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private int physicsWindDamage = 2;
    [SerializeField] private float physicsBulletGravity = 3f;

    [SerializeField] private float impactForce = 5f;

    [SerializeField] private AudioSource windTriggerSFX;
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
            damage = normalWindDamage;
        }
        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage = physicsWindDamage;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysWind.value & (1 << collision.gameObject.layer)) > 0)
        {
            //SFX
            windTriggerSFX.Play();
            //Explosion Animation
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Explode");
            }
            rb.velocity = Vector2.zero;
            // Wait for the duration of the explosion animation
            StartCoroutine(DestroyAfterDelay(2f));

            //Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.TakeDamage((int)damage);
                Debug.Log(damage);
            }
            Debug.Log("Hit something");

            //push object
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
                enemyRb.AddForce(forceDirection * impactForce, ForceMode2D.Impulse);
            }

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
        rb.velocity = transform.right * normalWindSpeed;
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
