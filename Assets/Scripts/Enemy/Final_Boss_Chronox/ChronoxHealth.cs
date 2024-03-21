using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoxHealth : BaseEnemy
{
    [Header("PhaseChange Parameter")]
    [SerializeField] private int changePhaseWhenHPReach;
    
    [Header("References")]
    public LayerMask playerLayer;
    public Animator animator;

    private ChronoxMovementAndShoot chronoxMovement;
    private ChronoxVirusBlasterSpawner spawner;

    private int phase;
    private int flag = 0;
    void Start()
    {
        currHealth = MaxHP();
        chronoxMovement = GetComponent<ChronoxMovementAndShoot>();
        spawner = GetComponent<ChronoxVirusBlasterSpawner>();
        animator = GetComponent<Animator>();
        phase = 1;
        isDead = false;
    }

    
    void Update()
    {
        if(flag == 0)
        {
            spawner.SpawnAllVirusBlaster(phase);
            flag = 1;
        }
    }

    public int Phase()
    {
        return phase;
    }

    public override void TakeDamage(int damageAmount)
    {
        if(phase == 0)
        {
            return;
        }

        currHealth -= damageAmount;
        if (currHealth <= changePhaseWhenHPReach && phase != 2)
        {
            StartCoroutine(GoNextPhase());
            Debug.Log("Phase 2");
        }
        else if(currHealth <= 0)
        {
            isDead = true;
            Debug.Log("Dead");

            
        }
    }

    public IEnumerator GoNextPhase()
    {
        int temp = phase;
        phase = 0;
        animator.SetTrigger("PhaseChange");

        yield return new WaitForSeconds(3f);

        phase = temp + 1;
        spawner.SpawnAllVirusBlaster(phase);
    }

    private IEnumerator Death()
    {

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
