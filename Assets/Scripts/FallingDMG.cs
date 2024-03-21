using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDMG : MonoBehaviour
{
    private int damage = 250;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth health = collider.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }
    }
}
