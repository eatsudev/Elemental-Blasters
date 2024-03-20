using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistDialogue : MonoBehaviour
{
    public GameObject npcCanvasDialogue;
    public GameObject npcCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "npc")
        {
            npcCanvasDialogue.SetActive(true);
        }
    }

    public void NextDialogue()
    {

    }

    public void CloseDialogue()
    {
        npcCanvasDialogue.SetActive(false);
        npcCollider.SetActive(false);
    }
}
