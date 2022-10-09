using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed;
    public Transform scope;

    private Transform target;
    private Rigidbody2D boatrb;

    private bool mayFire;

    public GameObject projectilPrefab;
    public Transform firePoint;
    public float cadence;
    private float shooterCounter;

    
    private void Start()
    {
        target = FindObjectOfType<Boat>().transform;
        boatrb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {

        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

        transform.up = direction;


        RaycastHit2D hit = Physics2D.Raycast(scope.position, direction);
        Debug.DrawRay(scope.position, direction, Color.red);

        if (hit.transform == target)
        {
            if (hit.distance > 10) boatrb.velocity = direction * speed;
            else
            {
                boatrb.velocity = Vector2.zero;
                shoot();
            }
        }
    }


    void shoot()
    {
        if (mayFire)
        {
            //Instantiate(projectilPrefab, firePoint.position, firePoint.rotation);
            var projectile = projectileControl.PC.setProjectile();
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);

            mayFire = false;
        }

        if (!mayFire)
        {
            shooterCounter -= Time.deltaTime;
            if (shooterCounter <= 0)
            {
                shooterCounter = cadence;
                mayFire = true;
            }
        }
    }
}
