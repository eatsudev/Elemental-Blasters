using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElephantEnemy : BaseEnemy
{
    
    [Header("Bullet Parameter")]
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float numberOfBullet;
    [SerializeField] private int numberOfShoot;

    [Header("Attack Parameters")]
    [SerializeField] private int sa;

    [Header("Agro Parameter")]
    [SerializeField] private float aggroHeight;
    [SerializeField] private float aggroWidth;
    
    [Header("Reference")]
    public CircleCollider2D rangeCollider;
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject shootPoint;
    public GameObject gun;


    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private GameObject spawnedBullets;

    private bool isShooting;
    private bool isCharging;
    private int amountAlreadyShot;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = MaxHP();
        isShooting = false;
        isCharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShooting && !isCharging)
        {
            CheckAggro();
            Debug.Log("A");
        }

        Debug.Log(isShooting);
    }

    private void CheckAggro()
    {
        RaycastHit2D[] ray = Physics2D.BoxCastAll(transform.position, new Vector2(aggroWidth, aggroHeight), 0, Vector2.right, playerLayer);

        foreach (RaycastHit2D hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                targetPos = hit.transform.position;
                targetedPlayer = hit.transform.gameObject.GetComponent<PlayerHealth>();
                CheckState();
                Debug.Log(targetedPlayer);
            }
        }


        
    }

    private void CheckState()
    {
        if(amountAlreadyShot >= numberOfShoot)
        {
            amountAlreadyShot = 0;
            isCharging = true;

        }
        else
        {
            isShooting = true;
            amountAlreadyShot++;
            StartCoroutine(StartShootingProcess());
        }
    }

    private IEnumerator StartChargingProcess()
    {
        yield return new WaitForSeconds(1f);

        isCharging = false;
    }

    private IEnumerator StartShootingProcess()
    {
        for (int i = 0; i <= numberOfBullet; i++)
        {

            Vector3 target = targetedPlayer.transform.position;
            Vector3 objectPosition = gun.transform.position;

            target.x = target.x - objectPosition.x;
            target.y = target.y - objectPosition.y;


            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            float randRot = Random.Range(-1f, 1f);
            Shoot(target, randRot);

            yield return new WaitForSeconds(shootDelay);

            Debug.Log(target);
        }

        yield return new WaitForSeconds(shootCooldown);

        isShooting = false;
    }

    private void Shoot(Vector2 direction, float randRot)
    {
        //Quaternion zRot = Quaternion.LookRotation(direction);

        spawnedBullets = Instantiate(bulletPrefabs, shootPoint.transform.position, gun.transform.rotation);

        ElephantBullets spawnedBulletsScript = spawnedBullets.GetComponent<ElephantBullets>();

        spawnedBulletsScript.parent = this;
        spawnedBulletsScript.lifetime = lifetime;
        spawnedBulletsScript.speed = speed;
    }
}
