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
    [SerializeField] private float shootCooldown;
    [SerializeField] private float numberOfLaser;
    [SerializeField] private int numberOfShoot;

    [Header("Flight Position and Parameters")]
    [SerializeField] private ChronoxFlightPoint[] flightPos;
    [SerializeField] private float timeUntilChangingPos;
    [SerializeField] private float speed;

    [Header("References")]
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject gun;
    public GameObject shootPoint;

    private PlayerHealth playerHealth;
    private Animator animator;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private ChronoxHealth chronoxHealth;
    private GameObject spawnedLaser;

    private float shootTimer;
    private float timeUntilChangingPosTimer;
    private bool reachedFlightPos;
    private bool isShooting;
    void Start()
    {
        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>();

        rb2d = GetComponent<Rigidbody2D>();
        chronoxHealth = GetComponent<ChronoxHealth>();
        animator = GetComponent<Animator>();

        timeUntilChangingPosTimer = 0f;
        reachedFlightPos = true;
        flightPos = FindObjectsOfType<ChronoxFlightPoint>();
    }

    void Update()
    {
        timeUntilChangingPosTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if (chronoxHealth.Phase() == 1)
        {
            if (timeUntilChangingPosTimer >= timeUntilChangingPos && reachedFlightPos)
            {
                int temp = Random.Range(0, flightPos.Length);

                FlytToSetPosition(flightPos[temp]);
            }
            if (!isShooting)
            {
                StartCoroutine(StartShootingProcess());
            }

        }
        else if (chronoxHealth.Phase() == 2)
        {

        }
    }
    
    private void FlytToSetPosition(ChronoxFlightPoint targetFlightPos)
    {
        StartCoroutine(MoveTo(targetFlightPos.transform.position));
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        reachedFlightPos = false;

        while (Mathf.Abs(Vector2.Distance(transform.position, target)) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        reachedFlightPos = true;
        timeUntilChangingPosTimer = 0f;
    }

    private IEnumerator StartShootingProcess()
    {
        isShooting = true;

        float isRight = playerHealth.transform.position.x > transform.position.x ? 1f : -1f;
        transform.localScale = new Vector3(isRight * 1f, transform.localScale.y, transform.localScale.z);
        gun.transform.localScale = new Vector3(isRight * 1f, gun.transform.localScale.y, gun.transform.localScale.z);

        for (int i = 0; i <= numberOfLaser; i++)
        {

            Vector3 target = playerHealth.transform.position;
            Vector3 objectPosition = gun.transform.position;

            target.x = target.x - objectPosition.x;
            target.y = target.y - objectPosition.y;


            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            float randRot = Random.Range(-1f, 1f);
            Shoot(target, randRot);

            yield return new WaitForSeconds(shootDelay);
        }

        yield return new WaitForSeconds(shootCooldown);

        isShooting = false;
    }

    private void Shoot(Vector2 direction, float randRot)
    {
        spawnedLaser = Instantiate(laserPrefabs, shootPoint.transform.position, gun.transform.rotation);

        ChronoxLasers spawnedLasersScript = spawnedLaser.GetComponent<ChronoxLasers>();

        spawnedLasersScript.parent = this;
        spawnedLasersScript.lifetime = lifetime;
        spawnedLasersScript.speed = speed;
    }

    public int LaserDamage()
    {
        return laserDamage;
    }
}
