using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private Transform barTransform;
    [SerializeField] private GameObject root;

    [SerializeField] private GameObject boatPosition;


    private void HandleLifeChanged(int obj)
    {
        barTransform.localScale = new Vector3(lifeManager.GetLifeNormalized(), 1, 1);
        root.gameObject.SetActive(!lifeManager.isFullLife());

        if(lifeManager.Life <= 0) lifeManager.OnLifeChanged -= HandleLifeChanged;
    }

    private void FixedUpdate()
    {
        var posY = boatPosition.transform.position.y + 1;
        Vector2 newPos = new Vector3(boatPosition.transform.position.x, posY);

        transform.position = newPos;
    }


    public void setBoat(LifeManager LM)
    {
        lifeManager = LM;

        //
        lifeManager.OnLifeChanged += HandleLifeChanged;
        lifeManager.onDie += HandleDie;
        boatPosition = lifeManager.gameObject;
        root.gameObject.SetActive(false);
        //
    }

    public void HandleDie()
    {
        lifeManager.onDie -= HandleDie;
        gameObject.SetActive(false);
    }


}
