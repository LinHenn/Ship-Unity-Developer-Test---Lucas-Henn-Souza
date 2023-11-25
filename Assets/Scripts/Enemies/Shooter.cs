using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private float speed;
    [HideInInspector] public int damage;

    [SerializeField] private Transform scope;

    private Transform target;
    private Rigidbody2D boatrb;

    private bool mayFire;

    [SerializeField] private GameObject projectilPrefab;
    [SerializeField] private Transform firePoint;
    public float cadence;
    private float shooterCounter;

    
    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Boat>().transform;
        boatrb = GetComponent<Rigidbody2D>();

        speed = lifeManager.characterLifeData.moveSpeed;
        damage = lifeManager.characterLifeData.damage;
    }


    private void FixedUpdate()
    {
        if (lifeManager.Life <= 0) return;

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
            var projectile = projectileControl.instance.setProjectile();
            if (projectile == null) { mayFire = false; return; }

            projectile.GetComponent<projectil>().damage = damage;
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
