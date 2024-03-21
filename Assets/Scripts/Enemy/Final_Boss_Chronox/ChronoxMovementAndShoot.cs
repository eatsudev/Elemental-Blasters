using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxMovementAndShoot : MonoBehaviour
{
    [Header("Laser Parameter")]
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float numberOfLaser;
    [SerializeField] private int numberOfShoot;

    [Header("Flight Position and Parameters")]
    [SerializeField] private ChronoxFlightPoint[] flightPos;
    [SerializeField] private float timeUntilChangingPos;
    [SerializeField] private float speed;

    [Header("References")]
    public LayerMask playerLayer;
    public Animator animator;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;
    private ChronoxHealth chronoxHealth;

    private float cooldownTimer;
    private float timeUntilChangingPosTimer;
    private bool reachedFlightPos;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        chronoxHealth = GetComponent<ChronoxHealth>();
        timeUntilChangingPosTimer = 0f;
        reachedFlightPos = true;
        flightPos = FindObjectsOfType<ChronoxFlightPoint>();
    }

    void Update()
    {
        timeUntilChangingPosTimer += Time.deltaTime;

        if (chronoxHealth.Phase() == 1)
        {
            if (timeUntilChangingPosTimer >= timeUntilChangingPos && reachedFlightPos)
            {
                int temp = Random.Range(0, flightPos.Length);

                FlytToSetPosition(flightPos[temp]);
            }
        }
        else if (chronoxHealth.Phase() == 2)
        {

        }
    }

    private void FlytToSetPosition(ChronoxFlightPoint targetFlightPos)
    {
        StartCoroutine(MoveTo(targetFlightPos.transform.position));
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        reachedFlightPos = false;

        while (Mathf.Abs(Vector2.Distance(transform.position, target)) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        reachedFlightPos = true;
        timeUntilChangingPosTimer = 0f;
    }

}
