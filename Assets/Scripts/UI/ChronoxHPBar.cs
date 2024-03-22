using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronoxHPBar : MonoBehaviour
{
    public Slider slider;
    public ChronoxHealth chronoxHealth;

    private void Start()
    {
        chronoxHealth = FindObjectOfType<ChronoxHealth>();

        slider.maxValue = chronoxHealth.MaxHP();
    }

    private void Update()
    {
        slider.value = chronoxHealth.currHealth;
    }
}
