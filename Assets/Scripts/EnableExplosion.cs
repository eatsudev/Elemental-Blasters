using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableExplosion : MonoBehaviour
{
    public GameObject explosion;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explosion.SetActive(true);
        }
    }
}
