using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnding : MonoBehaviour
{
    public GameObject flash;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "explosionproj")
        {
            flash.SetActive(true);
        }
    }
}
