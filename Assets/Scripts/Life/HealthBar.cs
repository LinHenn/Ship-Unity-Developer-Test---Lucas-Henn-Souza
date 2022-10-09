using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private Transform barTransform;
    [SerializeField] private GameObject root;


    void Awake()
    {
        lifeManager.OnLifeChanged += HandleLifeChanged;
        root.gameObject.SetActive(false);
    }

    private void HandleLifeChanged(int obj)
    {
        barTransform.localScale = new Vector3(lifeManager.GetLifeNormalized(), 1, 1);
        root.gameObject.SetActive(!lifeManager.isFullLife());
    }

    
    void Update()
    {
        
    }
}
