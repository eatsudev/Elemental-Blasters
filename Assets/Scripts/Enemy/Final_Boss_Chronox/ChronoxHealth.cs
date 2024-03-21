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

    private ChronoxMovementAndShoot chronoxMovementAndShoot;
    private ChronoxVirusBlasterSpawner spawner;

    private int phase;
    public int flag = 0;
    void Start()
    {
        currHealth = MaxHP();
        chronoxMovementAndShoot = GetComponent<ChronoxMovementAndShoot>();
        spawner = GetComponent<ChronoxVirusBlasterSpawner>();

        chronoxMovementAndShoot.enabled = false;

        phase = 1;
        isDead = false;
    }

    
    void Update()
    {
        if(flag == 0)
        {
            animator.SetTrigger("throwVirus");
            StartCoroutine(spawner.SpawnAllVirusBlaster(phase));
            

        }
        if (flag == 1)
        {
            chronoxMovementAndShoot.enabled = true;
            animator.ResetTrigger("throwVirus");
            flag++;
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
        animator.SetTrigger("phaseChange");

        yield return new WaitForSeconds(2f);

        phase = temp + 1;
        spawner.SpawnAllVirusBlaster(phase);
    }

    private IEnumerator Death()
    {

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
