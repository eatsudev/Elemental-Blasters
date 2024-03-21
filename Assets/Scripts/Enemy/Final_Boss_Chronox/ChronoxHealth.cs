using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxHealth : BaseEnemy
{
    [Header("PhaseChange Parameter")]
    [SerializeField] private int changePhaseWhenHPReach;
    
    [Header("References")]
    public LayerMask playerLayer;
    public Animator animator;

    private ChronoxMovement ChronoxMovement;

    private bool isDead;
    private int phase;
    void Start()
    {
        currHealth = MaxHP();
        ChronoxMovement = GetComponent<ChronoxMovement>();
        animator = GetComponent<Animator>();
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Phase()
    {
        return phase;
    }

    public override void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth <= changePhaseWhenHPReach && !isDead)
        {
            GoNextPhase();
        }
        else if(currHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void GoNextPhase()
    {
        phase++;
        animator.SetTrigger("PhaseChange");
    }
}
