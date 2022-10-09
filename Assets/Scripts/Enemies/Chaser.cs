using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float speed;
    public Transform scope;

    public BoxCollider2D box;

    private Transform target;
    private Rigidbody2D boatrb;
    



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

        if (hit.transform == target) boatrb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Boat>(out var boat))
        {
            Debug.Log("encostei");
            boat.lifeManager.TakeDamage(2);
            Destroy(gameObject);
        }

    }






}
