using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollect : MonoBehaviour
{
    [SerializeField] private GameObject earth;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            earth.SetActive(true);
            Destroy(gameObject);
        }
    }
}
