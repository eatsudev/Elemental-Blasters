using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCity2 : MonoBehaviour
{
    public GameObject gate;
    public Animator animator;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gate.SetActive(false);
            animator.SetTrigger("Pressed");
        }
    }
}
