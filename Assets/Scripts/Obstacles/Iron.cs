using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : BaseObstacleIron
{
    //public Animator animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //StartCoroutine(DelayBeforeBurn(2.5f));
        if (collision.gameObject.tag == "water")
        {
            //animator.SetTrigger("Bakar");
        }
    }

    private IEnumerator DelayBeforeBurn(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Destroy(gameObject);
    }
}
