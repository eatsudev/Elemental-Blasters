using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemyAggro : MonoBehaviour
{
    [SerializeField] private RatEnemy ratEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHealth>() != null)
        {
            ratEnemy.Activate(collision.transform.gameObject.GetComponent<PlayerHealth>());
        }
    }
}
 