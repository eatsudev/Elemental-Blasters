using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlinking : MonoBehaviour
{
    public Light2D light2D;
    public float blinkInterval = 0.5f;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            light2D.enabled = true;

            yield return new WaitForSeconds(blinkInterval);

            light2D.enabled = false;

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
