using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxDialogue : MonoBehaviour
{
    public GameObject chronoxCanvas1;
    public GameObject chronoxCanvas2;
    public GameObject chronoxCanvas3;
    public GameObject chronoxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "chronox")
        {
            chronoxCanvas1.SetActive(true);
        }
    }

    public void ChronoxNextDialogue2()
    {
        chronoxCanvas1.SetActive(false);
        chronoxCanvas2.SetActive(true);
    }

    public void ChronoxNextDialogue3()
    {
        chronoxCanvas2.SetActive(false);
        chronoxCanvas3.SetActive(true);
    }

    public void ChronoxCloseDialogue()
    {
        chronoxCanvas3.SetActive(false);
        chronoxCollider.SetActive(false);
    }
}
