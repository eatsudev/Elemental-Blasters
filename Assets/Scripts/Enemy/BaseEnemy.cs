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


    virtual public void TakeDamage(int damageAmount)
    { 
        currHealth -= damageAmount;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Debug.Log("dead");
            Destroy(gameObject);
        }
            
    }

    virtual public void DealDamage()
    {

    }
}
