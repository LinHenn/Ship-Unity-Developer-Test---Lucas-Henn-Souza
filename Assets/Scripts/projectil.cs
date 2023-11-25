using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectil : MonoBehaviour
{
    public float speed = 8;
    public int damage;

    public GameObject explosionEffect;

    private void OnEnable()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.lifeManager.TakeDamage(damage);
        }


        if (collision.TryGetComponent<Boat>(out var boat))
        {
            boat.lifeManager.TakeDamage(damage);
        }





        Instantiate(explosionEffect, transform.position, transform.rotation);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    /*
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    */
}
