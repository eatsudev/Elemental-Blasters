using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ChronoxHealth : BaseEnemy
{
    [Header("PhaseChange Parameter")]
    [SerializeField] private int changePhaseWhenHPReach;
    [SerializeField] private int healthWhenActivatingUltimate;

    [Header("Ultimate Parameter")]
    [SerializeField] private Light2D ultimateLight;
    [SerializeField] private int ultimateTimeLength;

    [Header("References")]
    public LayerMask playerLayer;
    public Animator animator;
    public PlayerAimAndShoot playerAimAndShoot;

    private ChronoxMovementAndShoot chronoxMovementAndShoot;
    private ChronoxVirusBlasterSpawner spawner;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxColl;

    private bool alreadyUltimate;
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
        alreadyUltimate = false;
    }

    
    void Update()
    {
        if(flag == 0)
        {
            chronoxMovementAndShoot.enabled = false;
            animator.SetTrigger("throwVirus");
            StartCoroutine(spawner.SpawnAllVirusBlaster(phase));
        }
        if (flag == 2)
        {
            chronoxMovementAndShoot.enabled = true;
            animator.ResetTrigger("throwVirus");
            flag++;
        }

        /*if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ChronoxUltimate());
        }*/
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
        else if (currHealth <= healthWhenActivatingUltimate && phase == 2 && !alreadyUltimate)
        {
            StartCoroutine(ChronoxUltimate());
            alreadyUltimate = true;
            Debug.Log("Ultimate");
        }
        else if(currHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death());
        }
    }
    
    private IEnumerator Death()
    {
        animator.SetTrigger("death");

        phase = 3;

        rb2d = GetComponent<Rigidbody2D>();
        chronoxMovementAndShoot = GetComponent<ChronoxMovementAndShoot>();

        rb2d.gravityScale = 1f;
        boxColl.isTrigger = false;
        boxColl.offset = new Vector2(0f, -1f);
        boxColl.size = new Vector2(1.5f, 1f);
        rb2d.constraints = RigidbodyConstraints2D.None;

        chronoxMovementAndShoot.enabled = false;
        spawner.enabled = false;

        yield return new WaitForSeconds(2f);

        chronoxMovementAndShoot.enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(2f);

        //Death Cutscene;
        SceneManager.LoadScene("Ending");
    }

    public IEnumerator GoNextPhase()
    {
        int temp = phase;
        phase = 0;
        animator.SetTrigger("phaseChange");

        rb2d.gravityScale = 2f;
        boxColl.isTrigger = false;
        rb2d.constraints = RigidbodyConstraints2D.None;
        chronoxMovementAndShoot.gun.SetActive(false);

        yield return new WaitForSeconds(2f);

        boxColl.isTrigger = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.rotation = Quaternion.identity;
        chronoxMovementAndShoot.gun.SetActive(true);

        rb2d.gravityScale = 0;
        phase = temp + 1;
        flag = 0;
    }
    public IEnumerator ChronoxUltimate()
    {
        playerAimAndShoot.ChangeElementalState(false);
        chronoxMovementAndShoot.ActivateUltimate(true);
        StartCoroutine(chronoxMovementAndShoot.UltimateProcess());
        phase = 0;

        yield return new WaitForSeconds(1.2f);

        Light2D light = Instantiate(ultimateLight, transform.position, Quaternion.identity);

        light.intensity = 100f;

        float timer = 0f;

        while (timer < ultimateTimeLength)
        {
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(light.intensity, 0, timer / ultimateTimeLength);

            yield return new WaitForSeconds(Time.deltaTime);
        }


        playerAimAndShoot.ChangeElementalState(true);
        chronoxMovementAndShoot.ActivateUltimate(false);
        phase = 2;
    }

}
