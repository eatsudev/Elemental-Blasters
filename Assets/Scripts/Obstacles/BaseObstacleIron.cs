using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacleIron : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP = 1;
    [SerializeField] private int damage;
    public int currHealth;
    public Animator animator;

    private bool isDead;

    void Start()
    {
        currHealth = maxHP;
    }


    void Update()
    {

    }

    public int MaxHP()
    {
        return maxHP;
    }

    public int Damage()
    {
        return damage;
    }

    public void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth <= 0)
        {
            currHealth = 0;
            animator.SetTrigger("Karat");
            Debug.Log("obstacle destroyed");
            StartCoroutine(DelayBeforeDestroy(1.5f));

        }
    }

    private IEnumerator DelayBeforeDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}