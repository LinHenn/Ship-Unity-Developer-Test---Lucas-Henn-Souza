using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public Transform firePoint;

    public float cadence = 0.5f;
    private float shooterCounter = 0;
    private bool mayFire;

    private void Start()
    {
        Boat.boat.willFire += Shoot;
    }

    void Update()
    {

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

    private void Shoot()
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
    }
}
