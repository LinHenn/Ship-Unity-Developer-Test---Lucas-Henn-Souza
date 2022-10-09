using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Boat : MonoBehaviour
{
    public static Boat boat;    

    private Rigidbody2D rb2d;
    private Vector2 movement;

    public float moveSpeed;
    public float rotationSpeed;
    private float rotationAngle;
    [Range(0,1)] public float driftFactor;
    public float dragFactor = 3;
    public float maxSpeed = 5;
    [SerializeField] internal LifeManager lifeManager;

    public event Action willFire;

    private void Awake()
    {
        boat = this;
        lifeManager.onDie += HandleDie;
    }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space)) willFire.Invoke();

        if (lifeManager.Life <= 0) GameController.GM.isFinished();
    }

    private void FixedUpdate()
    {
        ApplySpeed();
        ApplyRotation();
        ApplyDrift();
    }

    void HandleDie()
    {
        Debug.Log("Morri");
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
