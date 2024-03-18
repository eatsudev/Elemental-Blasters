using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHP = 5f;
    [SerializeField] private int damage;
    private float currHealth;

    private bool isDead;

    private void Start()
    {
        currHealth = maxHP;
    }


    void Update()
    {
        
    }

    public int Damage()
    {
        return damage;
    }

    public void TakeDamage(float damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth < 0)
        {
            currHealth = 0;
            Destroy(gameObject);
            Debug.Log("dead");
        }
            
    }

    public void DealDamage()
    {

    }

    /*public void Damage(float damageAmount)
    {
        currHealth -= damageAmount;
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }*/
}
