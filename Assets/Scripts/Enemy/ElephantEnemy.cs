using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantEnemy : BaseEnemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float numberOfSpikes;

    public CircleCollider2D rangeCollider;
    public LayerMask playerLayer;

    private Rigidbody2D rb2d;
    private GameObject spawnedSpikes;
    private float spikesSpread;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = MaxHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
