using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCity3 : MonoBehaviour
{
    public GameObject gate;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gate.SetActive(false);
        }
    }
}
