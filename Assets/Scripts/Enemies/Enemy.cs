using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionEffect;
    public Transform explosionPosition;
    [SerializeField] internal LifeManager lifeManager;

    private void Awake()
    {
        lifeManager.onDie += HandleDie;
    }

    private void HandleDie()
    {
        GameController.GM.plusPoints();
        Destroy(transform.parent.gameObject, 1f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Boat>(out var boat))
        {
            Instantiate(explosionEffect, explosionPosition.position, transform.rotation);
        }
    }
}
