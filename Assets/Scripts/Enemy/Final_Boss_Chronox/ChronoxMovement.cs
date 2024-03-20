using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxMovement : MonoBehaviour
{
    [Header("Laser Parameter")]
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private float lifetime;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float numberOfLaser;
    [SerializeField] private int numberOfShoot;

    [Header("Attack Parameters")]
    [SerializeField] private float aggroHeight;
    [SerializeField] private float aggroWidth;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;

    [Header("FlightPosition")]
    [SerializeField] private GameObject[] flightPos;

    [Header("References")]
    public BoxCollider2D damageCollider;
    public LayerMask playerLayer;
    public Animator animator;

    private PlayerHealth targetedPlayer;
    private Vector2 targetPos;
    private Rigidbody2D rb2d;

    private float cooldownTimer;
    private bool isChasing;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
