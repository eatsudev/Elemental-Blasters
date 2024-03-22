using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBlaster : BaseEnemy
{
    [Header("Bullet Parameter")]
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float numberOfBullet;
    [SerializeField] private int numberOfShoot;

    [Header("Ammo Parameter")]
    [SerializeField] private int ammoAddedOnDeath;

    [Header("Reference")]
    public CircleCollider2D rangeCollider;
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject shootPoint;
    public GameObject gun;

    private PlayerHealth targetedPlayer;
    private PlayerAimAndShoot targetedPlayerShoot;
    private GameObject spawnedBullets;
    private float cooldownTimer;
    private float spawnTimer;
    public bool isSpawned;
    void Start()
    {
        currHealth = MaxHP();
        targetedPlayerShoot = FindObjectOfType<PlayerAimAndShoot>();
        spawnTimer = 0f;
    }

    void Update()
    {
        if (spawnTimer > 1f)
        {
            isSpawned = true;
        }
        else
        {
            spawnTimer += Time.deltaTime;
            return;
        }
        

        cooldownTimer += Time.deltaTime;
        
        if (InRange())
        {
            if(cooldownTimer >= shootCooldown)
            {
                cooldownTimer = 0f;
                Shoot(targetedPlayer.transform.position);
            }
        }
    }

    public void SetShootCooldown(float shootCooldown)
    {
        this.shootCooldown = shootCooldown;
    }

    private void Shoot(Vector2 target)
    {
        int isRight = target.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(isRight * 1f, 1f, 1f);
        gun.transform.localScale = new Vector3(isRight, 1f, 1f);

        RotateTowards(target);

        spawnedBullets = Instantiate(bulletPrefabs, shootPoint.transform.position, gun.transform.rotation);

        VirusBullets spawnedBulletsScript = spawnedBullets.GetComponent<VirusBullets>();

        spawnedBulletsScript.parent = this;
        spawnedBulletsScript.lifetime = lifetime;
        spawnedBulletsScript.speed = bulletSpeed;
    }

    private void RotateTowards(Vector2 target)
    {
        Vector3 objectPosition = gun.transform.position;

        target.x -= objectPosition.x;
        target.y -= objectPosition.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

                targetedPlayer = playerHealth;
                targetedPlayerShoot = hit.transform.gameObject.GetComponent<PlayerAimAndShoot>();
            }
        }

        bool temp = playerHealth != null;

        return temp;
    }

    public override void TakeDamage(int damageAmount)
    {
        if (!isSpawned)
        {
            return;
        }

        currHealth -= damageAmount;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Debug.Log("dead");
            targetedPlayerShoot.AddElementalAmmo(ammoAddedOnDeath);
            Destroy(gameObject);
        }
    }
}
