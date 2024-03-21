using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : BaseEnemy
{
    [SerializeField] private float explosionRange;
    [SerializeField] private float speed;
    [SerializeField] private float maxActiveTime;

    public BoxCollider2D aggroColider;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    public Animator animator;

    private bool isActive;
    private bool isExpoded;
    private float activeTime;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currHealth = MaxHP();
        isExpoded = false;
    }

    // Update is called once per frames
    void Update()
    {

        if (isActive && !isExpoded)
        {
            MoveToPosition();
            activeTime += Time.deltaTime;

            if (transform.position.x > targetPos.x - 0.1f && transform.position.x < targetPos.x + 0.1f)
            {
                Explode();
            }
        }

        /*if(activeTime > maxActiveTime)
        {
            Explode();
        }*/
    }

    private void MoveToPosition()
    {
        if(transform.position.x < targetPos.x)
        {
            rb2d.velocity = new Vector2(speed, 0f);
            animator.SetBool("isWalking", true);
            transform.parent.localScale = Vector3.one;
        }
        else if (transform.position.x > targetPos.x)
        {
            rb2d.velocity = new Vector2(-speed, 0f);
            animator.SetBool("isWalking", true);
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Explode()
    {
        animator.SetTrigger("explode");

        RaycastHit2D[] ray = Physics2D.CircleCastAll(transform.position, explosionRange, Vector2.zero);
        foreach (RaycastHit2D hit in ray)
        {
            if(hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage());

                Vector2 dir = (hit.transform.position - transform.position).normalized;

                hit.transform.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(dir * 30f, hit.transform.position, ForceMode2D.Impulse);

                Debug.Log(dir);
            }
        }

        isExpoded = true;
        rb2d.velocity = Vector2.zero;
        Destroy(rb2d);
        Destroy(boxCollider2D);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    public void Activate(PlayerHealth player)
    {
        isActive = true;
        targetedPlayer = player;
        targetPos = player.transform.position;
        activeTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            Explode();
        }
    }

}
