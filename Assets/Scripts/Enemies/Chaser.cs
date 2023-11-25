using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{ 
    private float speed;
    [HideInInspector] public int damage;

    [SerializeField] private Transform scope;

    public BoxCollider2D box;

    private Transform target;
    private Rigidbody2D boatrb;

    
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

        if (hit.transform == target) boatrb.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Boat>(out var boat))
        {
            boat.lifeManager.TakeDamage(damage);
            GetComponent<LifeManager>().TakeDamage(3);
        }

    }
}
