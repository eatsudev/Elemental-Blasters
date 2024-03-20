using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    [Header("Charge Parameters")]
    [SerializeField] private int chargeDamage;
    [SerializeField] private int chargeLength;
    [SerializeField] private int chargeDelay;
    [SerializeField] private int chargeCooldown;
    [SerializeField] private int chargeRange;
    [SerializeField] private int chargeSpeed;


    [Header("Agro Parameter")]
    [SerializeField] private float aggroHeight;
    [SerializeField] private float aggroWidth;
    
    [Header("Reference")]
    public LayerMask playerLayer;
    public LayerMask notDestroyable;
    public GameObject shootPoint;
    public GameObject gun;
    public BoxCollider2D chargeHurtBox;


    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private GameObject spawnedBullets;

    private bool isShooting;
    private bool isCharging;
    private bool isAlreadyHit;
    private int amountAlreadyShot;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = MaxHP();
        isShooting = false;
        isCharging = false;
        isAlreadyHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShooting && !isCharging)
        {
            CheckAggro();
        }

        if (isCharging && !isAlreadyHit)
        {
            Attack();
            Debug.Log("A");
        }


    }

    private void CheckAggro()
    {
        RaycastHit2D[] ray = Physics2D.BoxCastAll(transform.position, new Vector2(aggroWidth, aggroHeight), 0, Vector2.zero, playerLayer);

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
        if (amountAlreadyShot >= numberOfShoot)
        {
            amountAlreadyShot = 0;
            isCharging = true;
            StartCoroutine(StartChargingProcess());
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
        isAlreadyHit = false;
        float timer = 0f;
        int isRight = targetedPlayer.transform.position.x > transform.position.x? 1 : -1 ;
        transform.localScale = new Vector3 (isRight * 2f, 2f, 1f);
        gun.transform.localScale = new Vector3(isRight, 1f, 1f);

        yield return new WaitForSeconds(chargeDelay);

        while (timer < chargeLength)
        {
            rb2d.velocity = new Vector2(isRight * chargeSpeed, 0f);

            timer += Time.deltaTime;
        }

        yield return new WaitForSeconds(chargeCooldown);

        isCharging = false;
    }


    private void Attack()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(chargeHurtBox.transform.position, chargeHurtBox.bounds.size, 0, transform.localScale, chargeHurtBox.bounds.size.x * 2, playerLayer);

        foreach (RaycastHit2D raycastHit2D in hit)
        {
            Debug.Log(raycastHit2D.collider);
            if (raycastHit2D.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                raycastHit2D.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(chargeDamage);
                isAlreadyHit = true;
            }
            
        }

        
    }

    private IEnumerator StartShootingProcess()
    {
        float isRight = targetedPlayer.transform.position.x > transform.position.x ? 1f : -1f;
        transform.localScale = new Vector3(isRight * 2f, 2f, 1f);
        gun.transform.localScale = new Vector3(isRight, 1f, 1f);

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
