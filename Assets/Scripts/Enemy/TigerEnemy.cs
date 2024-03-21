using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerEnemy : BaseEnemy
{
    [Header("Attack Parameters")]
    [SerializeField] private float aggroHeight;
    [SerializeField] private float aggroWidth;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;

    [Header("References")]
    public BoxCollider2D damageCollider;
    public LayerMask playerLayer;
    public Animator animator;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;

    private float cooldownTimer;
    private bool isChasing;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = MaxHP();
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        

        CheckAgro();

        if (targetPos != null && isChasing && !PlayerInSight())
        {
            MoveToTargetPosition();
            animator.SetTrigger("Attack");
        }

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void MoveToTargetPosition()
    {
        if (transform.position.x < targetPos.x)
        {
            rb2d.velocity = new Vector2(speed, 0f);
            transform.localScale = Vector3.one;
        }
        else if (transform.position.x > targetPos.x)
        {
            rb2d.velocity = new Vector2(-speed, 0f);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(transform.position.x > targetPos.x - 0.1f && transform.position.x < targetPos.x + 0.1f)
        {
            isChasing = false;
        }
    }

    private void CheckAgro()
    {
        RaycastHit2D[] ray = Physics2D.BoxCastAll(transform.position, new Vector2(aggroWidth, aggroHeight), 0, Vector2.zero);
        foreach (RaycastHit2D hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                targetPos = hit.transform.position;
                isChasing = true;
            }
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(damageCollider.transform.position, damageCollider.bounds.size, 0, transform.right, damageCollider.bounds.size.y, playerLayer);

        if (hit.collider != null)
            targetedPlayer = hit.transform.GetComponent<PlayerHealth>();

        
        return hit.collider != null;
    }

    private void Attack()
    {
        if (PlayerInSight())
        {
            targetedPlayer.TakeDamage(Damage());
        }
    }
}
