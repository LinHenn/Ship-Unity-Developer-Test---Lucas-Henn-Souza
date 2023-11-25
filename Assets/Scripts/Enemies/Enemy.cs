using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionEffect;
    public Transform explosionPosition;
    internal LifeManager lifeManager;

    private bool isdead;


    private void Awake()
    {
        lifeManager = GetComponent<LifeManager>();
    }

    protected virtual void Start()
    {
        lifeManager.onDie += HandleDie;
    }

    private void OnEnable()
    {
        isdead = false;
    }

    private void HandleDie()
    {
        GameController.GM.plusPoints();

        if (isdead) return;
        StartCoroutine(timerDead());

    }

    IEnumerator timerDead()
    {
        isdead = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
