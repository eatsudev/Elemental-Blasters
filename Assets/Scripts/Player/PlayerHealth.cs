using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //private HPCounter hpCounter;
    [SerializeField] private int health = 250;
    public GameObject Dead;
    private bool isDead = false;
    public Animator animator;
    [SerializeField] private AudioSource playerHitSFX;
    public static PlayerHealth Instance { get; private set; }
    public int MAX_HEALTH = 250;
    public GameObject gunSprite;

    void Start()
    {
        health = MAX_HEALTH;
        //hpCounter = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HPCounter>();

        /*if (hpCounter == null)
        {
            Debug.LogError("HPCounter script not found on the Canvas GameObject.");
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Heal(50);
        }
    }

    private void Awake()
    {
        // Set the static instance reference when the object is instantiated
        Instance = this;
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        StartCoroutine(VisualIndicator(Color.red));
        playerHitSFX.Play();

        if (health <= 0)
        {
            Die();
        }

        // Update the HP counter with the new health value
        /*if (hpCounter != null)
        {
            hpCounter.UpdateHPCounter(health);
        }*/

    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Heal amount");
        }

        this.health += amount;

        health = Mathf.Min(health, MAX_HEALTH);

        // Update the HP counter with the new health value
        /*if (hpCounter != null)
        {
            hpCounter.UpdateHPCounter(health);
        }

        if (animator != null)
        {
            animator.SetBool("IsHealing", true);
            StartCoroutine(ResetHealingAnimation());
        }*/

    }

    private IEnumerator ResetHealingAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        if (animator != null)
        {
            animator.SetBool("IsHealing", false);
        }
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Player dead!");
            animator.SetTrigger("Death");
            gunSprite.SetActive(false);
            StartCoroutine(DeathPauseAndShowUI());
        }
    }

    private IEnumerator DeathPauseAndShowUI()
    {
        Dead.SetActive(true);
        //Time.timeScale = 0f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
}
