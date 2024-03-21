using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.TakeDamage((int)damage);
            Debug.Log(damage);
        }
    }
}
