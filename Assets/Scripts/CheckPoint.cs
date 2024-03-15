using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private SaveHandler handler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IUnit unit;
        if (collision.gameObject.GetComponent<IUnit>() != null)
        {
            unit = collision.gameObject.GetComponent<IUnit>();

            if (unit.GetLastCheckPoint() != this)
            {
                handler.SaveProcess(this);
            }
        }
    }
}