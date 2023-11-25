using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionEffect;
    public Transform explosionPosition;
    internal LifeManager lifeManager;


    private void Awake()
    {
        lifeManager = GetComponent<LifeManager>();
    }

    protected virtual void Start()
    {
        lifeManager.onDie += HandleDie;
    }

    private void HandleDie()
    {
        GameController.GM.plusPoints();
        StartCoroutine(timerDead());
        //gameObject.SetActive(false);
        //Destroy(gameObject, 1f);

    }

    IEnumerator timerDead()
    {
        //Debug.Log(gameObject.name);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        //StopCoroutine(timerDead());
    }

}
