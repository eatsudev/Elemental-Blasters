using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
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
    private Rigidbody2D rb2d;
    private BoxCollider2D boxColl;

    private int phase;
    public int flag = 0;
    void Start()
    {
        currHealth = MaxHP();
        chronoxMovementAndShoot = GetComponent<ChronoxMovementAndShoot>();
        spawner = GetComponent<ChronoxVirusBlasterSpawner>();
        rb2d = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();

        chronoxMovementAndShoot.enabled = false;

        phase = 1;
        isDead = false;
    }

    
    void Update()
    {
        if(flag == 0)
        {
            chronoxMovementAndShoot.enabled = false;
            animator.SetTrigger("throwVirus");
            StartCoroutine(spawner.SpawnAllVirusBlaster(phase));
        }
        if (flag == 1)
        {
            chronoxMovementAndShoot.enabled = true;
            animator.ResetTrigger("throwVirus");
            flag++;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Death());
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
        if (currHealth <= changePhaseWhenHPReach && phase < 2)
        {
            StartCoroutine(GoNextPhase());
            Debug.Log("Phase 2");
        }
        else if(currHealth <= 0)
        {
            isDead = true;
            StartCoroutine(Death());
        }
    }
    
    private IEnumerator Death()
    {
        animator.SetTrigger("death");

        phase = 3;
        rb2d.gravityScale = 1f;
        boxColl.isTrigger = false;
        boxColl.offset = new Vector2(-0.5f, -1f);
        boxColl.size = new Vector2(3.5f, 1f);
        rb2d.constraints = RigidbodyConstraints2D.None;

        chronoxMovementAndShoot = GetComponent<ChronoxMovementAndShoot>();

        chronoxMovementAndShoot.enabled = false;
        spawner.enabled = false;

        yield return new WaitForSeconds(2f);

        chronoxMovementAndShoot.enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        Debug.Log(chronoxMovementAndShoot.enabled);

        //Death Cutscene;
    }

    public IEnumerator GoNextPhase()
    {
        int temp = phase;
        phase = 0;
        animator.SetTrigger("phaseChange");

        rb2d.gravityScale = 1f;
        boxColl.isTrigger = false;
        rb2d.constraints = RigidbodyConstraints2D.None;

        yield return new WaitForSeconds(2f);

        boxColl.isTrigger = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        rb2d.gravityScale = 0;
        phase = temp + 1;
        flag = 0;
    }

}
