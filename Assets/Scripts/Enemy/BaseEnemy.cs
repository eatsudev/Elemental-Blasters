using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float healthPoint;
    [SerializeField] private int damage;

    private bool isDead;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public int Damage()
    {
        return damage;
    }

    public void TakeDamage(float damage)
    {
        healthPoint -= damage;
        if (healthPoint < 0)
        {
            healthPoint = 0;
            isDead = true;
            Debug.Log("dead");
        }
            
    }

    public void DealDamage()
    {

    }
}
