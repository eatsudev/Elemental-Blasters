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

    private PlayerHealth target;
    private Animator animator;
    private GameObject spawnedProjectile;
    private Vector3 originalScale;
    private float shootTimer;

    void Start()
    {
        currHealth = MaxHP();
        animator = GetComponent<Animator>();

        originalScale = transform.localScale;

        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>();
    }
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
                    animator.SetTrigger("attack");

                    ShootProcess();
                }
            }
            int isRight = playerHealth.transform.position.x > transform.position.x ? -1 : 1;
            transform.localScale = new Vector3(originalScale.x * isRight, originalScale.y, originalScale.z);
        }
        else
        {
            target = null;
        }
    }

    private bool InAggroRange()
    {
        bool isInAggroRange = Mathf.Abs(Vector2.Distance(playerHealth.transform.position, transform.position)) <= aggroRange;
        target = playerHealth;

        return isInAggroRange;
    }
    private bool InShootingRange()
    {
        bool isInShootingRange = Mathf.Abs(Vector2.Distance(playerHealth.transform.position, transform.position)) <= shootingRange;
        target = playerHealth;

        return isInShootingRange;
    }

    private void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void ShootProcess()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 objectPosition = transform.transform.position;

        targetPosition.x -= objectPosition.x;
        targetPosition.y -= objectPosition.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

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
