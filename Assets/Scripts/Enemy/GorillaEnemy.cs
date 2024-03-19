using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaEnemy : BaseEnemy
{
    public BoxCollider2D aggroColider;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private bool isActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            CheckAgro();
        }
    }

    private void TargetPosition(Vector2 targetPos)
    {
        
    }
    private void CheckAgro()
    {
        RaycastHit2D[] ray = Physics2D.CircleCastAll(transform.position, 5, Vector2.zero);
        foreach (RaycastHit2D hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<PlayerHealth>() != null)
            {
                TargetPosition(hit.transform.position);
                isActive = true;
            }
        }
    }
}
