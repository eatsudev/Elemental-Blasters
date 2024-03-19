using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCityManager : MonoBehaviour
{
    public GameObject bgmGameObject;

    void Update()
    {
        // Check if the player is dead and disable the BGM GameObject if it's dead
        if (PlayerHealth.Instance.GetCurrentHealth() <= 0)
        {
            bgmGameObject.SetActive(false);
        }
    }
}
