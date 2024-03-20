using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prof : MonoBehaviour
{
    public GameObject profCanvas1;
    public GameObject profCanvas2;
    public GameObject profCanvas3;
    public GameObject profCanvas4;
    public GameObject profCanvas5;

    public GameObject profHidup;
    public GameObject borderToNextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "prof")
        {
            profCanvas1.SetActive(true);
        }
    }

    public void NextDialogue2()
    {
        profCanvas1.SetActive(false);
        profCanvas2.SetActive(true);
    }

    public void NextDialogue3()
    {
        profCanvas2.SetActive(false);
        profCanvas3.SetActive(true);
    }

    public void NextDialogue4()
    {
        profCanvas3.SetActive(false);
        profCanvas4.SetActive(true);
    }

    public void NextDialogue5()
    {
        profCanvas4.SetActive(false);
        profCanvas5.SetActive(true);
    }

    public void CloseDialogue()
    {
        profCanvas5.SetActive(false);
        profHidup.SetActive(false);
        borderToNextScene.SetActive(false);
    }
}
