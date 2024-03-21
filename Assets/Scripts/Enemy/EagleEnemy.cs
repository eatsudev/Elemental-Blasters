using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EagleEnemy : BaseEnemy
{
    [Header("Projectile Parameter")]
    [SerializeField] private GameObject projectilePrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float featherSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float numberOfProjectile;
    [SerializeField] private float middleProjectileAheadTime;
    [SerializeField] private float projectilesOffset;

    [Header("Aggro Parameter")]
    [SerializeField] private float aggroRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private float speed;

    [Header("References")]
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject shootPoint;
    public GameObject shoot;
    public PlayerHealth playerHealth;


    private GameObject spawnedProjectile;
    private float shootTimer;
    private Vector3 projectilesOffsetVector;

    void Start()
    {
        currHealth = MaxHP();

        projectilesOffsetVector = new Vector3(0f, projectilesOffset, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (InAggroRange())
        {
            if (!InShootingRange())
            {
                MoveTo(playerHealth.transform.position);
            }
            else
            {
                if (shootTimer >= attackCooldown)
                {
                    shootTimer = 0;
                    ShootProcess(playerHealth);
                }
            }
        }
    }

    private bool InAggroRange()
    {
        bool isInRange = Mathf.Abs(Vector2.Distance(playerHealth.transform.position, transform.position)) <= aggroRange;

        return isInRange;
    }
    private bool InShootingRange()
    {
        bool isInRange = Mathf.Abs(Vector2.Distance(playerHealth.transform.position, transform.position)) <= shootingRange;
        
        return isInRange;
    }

    private void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }

    private void ShootProcess(PlayerHealth targetPlayer)
    {
        Vector3 target = targetPlayer.transform.position;
        Vector3 objectPosition = transform.transform.position;

        target.x = target.x - objectPosition.x;
        target.y = target.y - objectPosition.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        shootPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        StartCoroutine(Shoot(0f, 0f));
        StartCoroutine(Shoot(middleProjectileAheadTime, projectilesOffset));
        StartCoroutine(Shoot(middleProjectileAheadTime, -projectilesOffset));
    }

    private IEnumerator Shoot(float delay, float offset)
    {
        yield return new WaitForSeconds(delay);

        spawnedProjectile = Instantiate(projectilePrefabs, shootPoint.transform.position + (shoot.transform.up * offset), shootPoint.transform.rotation);

        Debug.Log(shoot.transform.up * projectilesOffset);

        EagleFeather eagleFeather = spawnedProjectile.GetComponent<EagleFeather>();
        eagleFeather.parent = this;
        eagleFeather.speed = speed;
        eagleFeather.lifetime = lifetime;
    }
}
