using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : BaseEnemy
{
    [SerializeField] private float explosionRange;
    [SerializeField] private float speed;

    public BoxCollider2D aggroColider;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private bool isActive;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MoveToPosition();
        }
    }

    private void MoveToPosition()
    {
        if(transform.position.x < targetPos.x)
        {
            rb2d.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb2d.velocity = new Vector2(-speed, 0f);
        }

        if(transform.position.x > aggroColider.bounds.size.x)
        {
            Explode();
        }
    }

    private void Explode()
    {
        RaycastHit2D[] ray = Physics2D.CircleCastAll(transform.position, 5, Vector2.zero);
        foreach (RaycastHit2D hit in ray)
        {
            if(hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage());
            }
            
        }
        
        Destroy(gameObject);
        this.enabled = false;
    }

    public void Activate(PlayerHealth player)
    {
        isActive = true;
        targetedPlayer = player;
        targetPos = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            Explode();
        }
    }

    
}
