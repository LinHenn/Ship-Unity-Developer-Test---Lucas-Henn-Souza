using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public Transform firePoint;
    [SerializeField] private int damage;


    private void Start()
    {
        Boat.instance.willFire += Shoot;
        damage = Boat.instance.damage;
    }

    private void Shoot()
    {
        var projectile = projectileControl.instance.setProjectile();
        if(projectile == null) return;
        projectile.GetComponent<projectil>().damage = damage;
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetActive(true);
    }
}
