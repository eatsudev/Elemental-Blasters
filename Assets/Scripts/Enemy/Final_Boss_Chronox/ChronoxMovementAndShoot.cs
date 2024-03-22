using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxMovementAndShoot : MonoBehaviour
{
    [Header("Laser Parameter")]
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private int laserDamage;
    [SerializeField] private float lifetime;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float phase1ShootCooldown;
    [SerializeField] private float phase2ShootCooldown;
    [SerializeField] private float timeBeforeGunDisappear;
    [SerializeField] private float numberOfLaser;
    [SerializeField] private int numberOfShoot;

    [Header("Flight Position and Parameters")]
    [SerializeField] private ChronoxFlightPoint[] flightPos;
    [SerializeField] private float timeUntilChangingPos;
    [SerializeField] private float phase1Speed;
    [SerializeField] private float phase2Speed;

    [Header("References")]
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject gun;
    public SpriteRenderer gunSprite;
    public GameObject shootPoint;
    public Animator animator;

    private PlayerHealth playerHealth;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private ChronoxHealth chronoxHealth;
    private GameObject spawnedLaser;

    private float shootTimer;
    private float timeUntilChangingPosTimer;
    private float ultimateTimer;
    private bool reachedFlightPos;
    private bool isShooting;
    private bool isActivatingUltimate;
    void Start()
    {
        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>();

        rb2d = GetComponent<Rigidbody2D>();
        chronoxHealth = GetComponent<ChronoxHealth>();

        timeUntilChangingPosTimer = 0f;
        reachedFlightPos = true;
        flightPos = FindObjectsOfType<ChronoxFlightPoint>();
        ultimateTimer = 0f;

        gunSprite.enabled = false;
    }

    void Update()
    {
        timeUntilChangingPosTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if (chronoxHealth.Phase() == 1 && !chronoxHealth.isDead && !isActivatingUltimate)
        {
            if (timeUntilChangingPosTimer >= timeUntilChangingPos && reachedFlightPos)
            {
                int temp = Random.Range(0, flightPos.Length);

                FlytToSetPosition(flightPos[temp], phase1Speed);
            }
            if (!isShooting)
            {
                StartCoroutine(StartShootingProcess(phase1ShootCooldown));
            }
        }
        else if (chronoxHealth.Phase() == 2 && !chronoxHealth.isDead && !isActivatingUltimate)
        {
            if (timeUntilChangingPosTimer >= timeUntilChangingPos && reachedFlightPos)
            {
                int temp = Random.Range(0, flightPos.Length);

                FlytToSetPosition(flightPos[temp], phase2Speed);
            }
            if (!isShooting)
            {
                StartCoroutine(StartShootingProcess(phase2ShootCooldown));
            }
        }
        else if(isActivatingUltimate)
        {
            ultimateTimer += Time.deltaTime;

        }
    }
    
    private void FlytToSetPosition(ChronoxFlightPoint targetFlightPos, float speed)
    {
        StartCoroutine(MoveTo(targetFlightPos.transform.position, speed));
    }

    private IEnumerator MoveTo(Vector2 target, float speed)
    {
        reachedFlightPos = false;

        animator.SetBool("isMoving", !reachedFlightPos);

        float isRight = target.x > transform.position.x ? 1f : -1f;
        transform.localScale = new Vector3(isRight * -1f, transform.localScale.y, transform.localScale.z);
        gun.transform.localScale = new Vector3(isRight * -1f, gun.transform.localScale.y, gun.transform.localScale.z);

        while (Mathf.Abs(Vector2.Distance(transform.position, target)) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        isRight = target.x > transform.position.x ? 1f : -1f;
        transform.localScale = new Vector3(isRight * -1f, transform.localScale.y, transform.localScale.z);
        gun.transform.localScale = new Vector3(isRight * -1f, gun.transform.localScale.y, gun.transform.localScale.z);

        reachedFlightPos = true;
        timeUntilChangingPosTimer = 0f;
        animator.SetBool("isMoving", !reachedFlightPos);
    }

    private IEnumerator StartShootingProcess(float shootCooldown)
    {
        isShooting = true;
        gunSprite.enabled = true;
        Debug.Log(gunSprite.enabled);

        float isRight = playerHealth.transform.position.x > transform.position.x ? 1f : -1f;
        transform.localScale = new Vector3(isRight * -1f, transform.localScale.y, transform.localScale.z);
        gun.transform.localScale = new Vector3(isRight * -1f, gun.transform.localScale.y, gun.transform.localScale.z);

        Debug.Log(isRight);

        animator.SetTrigger("shoot");

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < numberOfLaser; i++)
        {
            Vector3 target = playerHealth.transform.position;
            Vector3 objectPosition = gun.transform.position;

            target.x -= objectPosition.x;
            target.y -= objectPosition.y;


            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Shoot(target);

            yield return new WaitForSeconds(shootDelay);
        }

        yield return new WaitForSeconds(timeBeforeGunDisappear);

        gunSprite.enabled = false;
        Debug.Log(gunSprite.enabled);

        yield return new WaitForSeconds(shootCooldown - timeBeforeGunDisappear);

        isShooting = false;
    }

    private void Shoot(Vector2 direction)
    {
        spawnedLaser = Instantiate(laserPrefabs, shootPoint.transform.position, gun.transform.rotation);

        ChronoxLasers spawnedLasersScript = spawnedLaser.GetComponent<ChronoxLasers>();

        spawnedLasersScript.parent = this;
        spawnedLasersScript.lifetime = lifetime;
        spawnedLasersScript.speed = laserSpeed;
    }

    public void ActivateUltimate(bool state)
    {
        isActivatingUltimate = state;
    }

    public IEnumerator UltimateProcess()
    {
        yield return new WaitForSeconds(5f);
    }

    public int LaserDamage()
    {
        return laserDamage;
    }
}
