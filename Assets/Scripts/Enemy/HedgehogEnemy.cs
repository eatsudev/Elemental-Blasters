using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HedgehogEnemy : BaseEnemy
{
    [Header("Spikes Parameter")]
    [SerializeField] private GameObject spikePrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float numberOfSpikes;

    [Header("References")]
    public CircleCollider2D rangeCollider;
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public AudioSource attackSFX;

    private Rigidbody2D rb2d;
    private Animator animator;
    private GameObject spawnedSpikes;
    private float spikesSpread;
    private float cooldownTimer;
    
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currHealth = MaxHP();
        cooldownTimer = 0f;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (InRange())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("shoot");
            }
        }
    }

    private void Shoot()
    {
        if(spikePrefabs != null)
        {
            spikesSpread = 180f / numberOfSpikes;

            AttackPatern(spikesSpread, numberOfSpikes);

        }
    }
    private void AttackPatern(float zRotation, float numberOfRepetition)
    {
        for(int i = 0; i < numberOfRepetition; i++)
        {
            Quaternion rot = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, i * zRotation + zRotation/2);

            spawnedSpikes = Instantiate(spikePrefabs, transform.position, rot);

            HedgehogSpikes hedgehogSpikes = spawnedSpikes.GetComponent<HedgehogSpikes>();
            hedgehogSpikes.parent = this;
            hedgehogSpikes.speed = speed;
            hedgehogSpikes.lifetime = lifetime;
        }

        attackSFX.Play();
    }

    private bool InRange()
    {
        RaycastHit2D[] raycastHit2D = Physics2D.CircleCastAll(transform.position, rangeCollider.radius, Vector2.up, playerLayer);

        PlayerHealth playerHealth = null;

        foreach (RaycastHit2D hit in raycastHit2D)
        {
            if (hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                playerHealth = hit.transform.gameObject.GetComponent<PlayerHealth>();
            }
        }

        return playerHealth != null;
    }
}
