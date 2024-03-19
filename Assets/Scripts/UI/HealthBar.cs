using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        slider.maxValue = playerHealth.MAX_HEALTH;
    }

    private void Update()
    {
        slider.value = playerHealth.GetCurrentHealth();
    }
}
