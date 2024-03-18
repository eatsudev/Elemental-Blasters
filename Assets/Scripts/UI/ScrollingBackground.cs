using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer backgroundRenderer;
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        //backgroundRenderer.material.mainTextureOffset += new Vector2(0, Time.deltaTime * speed);
    }
}
