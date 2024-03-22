using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgDialogue : MonoBehaviour
{
    public GameObject cyborgCanvas1;
    public GameObject cyborgCanvas2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cyborg")
        {
            cyborgCanvas1.SetActive(true);
        }
    }

    public void CyborgNextDialogue2()
    {
        cyborgCanvas1.SetActive(false);
        cyborgCanvas2.SetActive(true);
    }

    public void CyborgCloseDialogue()
    {
        cyborgCanvas2.SetActive(false);
    }
}
