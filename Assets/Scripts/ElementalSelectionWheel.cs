using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementalSelectionWheel : MonoBehaviour
{
    public int ID;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    public Sprite icon;

    private ElementalWheelController controller;
    private Animator animator;
    private bool selected = false;
    [SerializeField] private AudioSource elementPressedSFX;

    void Start()
    {
        controller = FindObjectOfType<ElementalWheelController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        selected = true;
        controller.elementID = ID;
        elementPressedSFX.Play();
    }
    public void Deselected()
    {
        selected = false;
        controller.elementID = 0;
    }
    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        itemText.text = itemName;
    }
    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        itemText.text = "";
    }
}
