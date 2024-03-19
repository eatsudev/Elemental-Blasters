using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HedgehogEnemy : BaseEnemy
{
    [SerializeField] private GameObject spikePrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float numberOfSpikes;

    public CircleCollider2D rangeCollider;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;

    private Rigidbody2D rb2d;
    private GameObject spawnedSpikes;
    private float spikesSpread;
    private float cooldownTimer;
    
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = MaxHP();

    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (InRange())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Shoot();
            }
        }
       
    }

    private void Shoot()
    {
        if(spikePrefabs != null)
        {
            spikesSpread = 180f / numberOfSpikes;

            AttackPatern(spikesSpread, numberOfSpikes);

            Debug.Log("Shoot");

        }
    }
    private void AttackPatern(float zRotation, float numberOfRepetition)
    {
        for(int i = 0; i < numberOfSpikes; i++)
        {
            Quaternion rot = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, i * zRotation + zRotation/2);

            spawnedSpikes = Instantiate(spikePrefabs, transform.position, rot);

            HedgehogSpikes hedgehogSpikes = spawnedSpikes.GetComponent<HedgehogSpikes>();
            hedgehogSpikes.parent = this;
            hedgehogSpikes.speed = speed;
        }
    }

    private bool InRange()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, rangeCollider.radius, Vector2.up, playerLayer);

        PlayerHealth playerHealth = raycastHit2D.transform.gameObject.GetComponent<PlayerHealth>();

        Debug.Log(raycastHit2D.transform.gameObject);

        Debug.Log(playerHealth != null);

        return playerHealth != null;
    }
}
