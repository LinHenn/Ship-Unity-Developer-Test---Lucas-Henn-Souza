using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Boat : MonoBehaviour
{
    public static Boat instance;    

    private Rigidbody2D rb2d;
    private Vector2 movement;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private float rotationAngle;
    [Range(0,1)] public float driftFactor;
    public float dragFactor = 3;
    public float maxSpeed = 5;
    public int damage = 1;

    internal LifeManager lifeManager;

    public event Action willFire;


    private float cadence = 0.5f;
    private float shooterCounter = 0;
    private bool mayFire;


    private void Awake()
    {
        lifeManager = GetComponent<LifeManager>();
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        
        
    }

    private void Start()
    {
        lifeManager.onDie += HandleDie;
    }


    void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");




        if (Input.GetButtonDown("Fire1") && mayFire) { willFire.Invoke(); mayFire = false; }

        //
        if (!mayFire)
        {
            shooterCounter -= Time.deltaTime;
            if (shooterCounter <= 0)
            {
                shooterCounter = cadence;
                mayFire = true;
            }
        }
        //
    }

    private void FixedUpdate()
    {
        ApplySpeed();
        ApplyRotation();
        ApplyDrift();
    }

    void HandleDie()
    {
        Debug.Log("You Are Dead");
    }

    void ApplySpeed()
    {
        if(movement.y == 0)
        {
            rb2d.drag = Mathf.Lerp(rb2d.drag, dragFactor, Time.fixedDeltaTime);
        }
        else
        {
            rb2d.drag = 0;
        }

        var velocityUp = Vector2.Dot(transform.up, rb2d.velocity);
        if (velocityUp > maxSpeed) return;
        if (velocityUp < (-maxSpeed * 0.5f)) return;


        rb2d.AddForce(transform.up * movement.y * moveSpeed, ForceMode2D.Force);
    }


    void ApplyRotation()
    {
        rotationAngle = rotationAngle - (movement.x * rotationSpeed);
        rb2d.MoveRotation(rotationAngle);
    }

    void ApplyDrift()
    {
        Vector2 velocityUp = transform.up * Vector2.Dot(rb2d.velocity, transform.up);
        Vector2 velocityRight = transform.right * Vector2.Dot(rb2d.velocity, transform.right);
        rb2d.velocity = velocityUp + velocityRight * driftFactor;
    }



}
