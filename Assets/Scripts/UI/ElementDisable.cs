using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDisable : MonoBehaviour
{
    public GameObject fire;
    public GameObject water;
    public GameObject shock;
    public GameObject wind;
    public GameObject earth;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "disablefire")
        {
            //fire.SetActive(false);
            water.SetActive(true);
        }

        if(collision.gameObject.tag == "disablewater")
        {
            //water.SetActive(false);
            earth.SetActive(true);
        }

        if(collision.gameObject.tag == "disableearth")
        {
            //earth.SetActive(false);
            shock.SetActive(true);
        }

        if(collision.gameObject.tag == "disableshock")
        {
            //shock.SetActive(false);
            wind.SetActive(true);
        }

        if(collision.gameObject.tag == "enableall")
        {
            //fire.SetActive(true);
            //water.SetActive(true);
            //earth.SetActive(true);
            //shock.SetActive(true);
        }
    }
}
