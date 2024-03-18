using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int damage;
    public int currHealth;

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
        if (currHealth < 0)
        {
            currHealth = 0;
            Debug.Log("dead");
            Destroy(gameObject);
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
