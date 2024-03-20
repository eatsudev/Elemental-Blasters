using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBehaviour : MonoBehaviour
{
    [SerializeField] private float normalDirtSpeed = 15f;
    [SerializeField] private int normalDirtDamage = 1;
    [SerializeField] private float destroyTime = 13f;
    [SerializeField] private LayerMask whatDestroysDirt;
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private int physicsDirtDamage = 2;
    [SerializeField] private float physicsBulletGravity = 3f;
    [SerializeField] private AudioSource earthTriggerSFX;
    [SerializeField] private GameObject earthPlatform;
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
            //transform.right = rb.velocity;
            transform.right = GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void InitializeBulletStats()
    {
        if (bulletType == BulletType.Normal)
        {
            SetStraightVelocity();
            damage = normalDirtDamage;
        }
        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage = physicsDirtDamage;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysDirt.value & (1 << collision.gameObject.layer)) > 0)
        {
            //SFX
            earthTriggerSFX.Play();
            //Explosion Animation
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Explode");
            }

            // Stop projectile movement and make collider
            rb.velocity = Vector2.zero;
            earthPlatform.SetActive(true);

            //Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.TakeDamage((int)damage);
                Debug.Log(damage);
            }
            Debug.Log("Hit something");
            //StartCoroutine(DestroyAfterDelay(13f));
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
        //rb.velocity = transform.right * normalDirtSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.right * normalDirtSpeed;
    }

    private void SetPhysicsVelocity()
    {
        //rb.velocity = transform.right * physicsBulletSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.right * physicsBulletSpeed;
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
