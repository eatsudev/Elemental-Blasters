using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : BaseEnemy
{
    [SerializeField] private float explosionRange;

    public BoxCollider2D aggroColider;

    private Vector2 targetPos;
    private bool isActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector2.MoveTowards(this.transform.position, targetPos, aggroColider.bounds.size.x);
        }
    }

    private void Explode()
    {
        RaycastHit2D[] ray = Physics2D.CircleCastAll(transform.position, 5, Vector2.zero);
        foreach (RaycastHit2D hit in ray)
        {
            hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage());
        }
    }

    public void Activate(Vector2 targetPos)
    {
        isActive = true;
        this.targetPos = targetPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            Explode();
        }
    }
}
