using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barCtrl : MonoBehaviour
{
    public static barCtrl instance;
    [SerializeField] private List<HealthBar> bars;


    private void Awake()
    {
        instance = this;
    }

    public void setBar(LifeManager LM)
    {

        foreach (var bar in bars)
        {
            if (!bar.gameObject.activeSelf)
            {
                bar.setBoat(LM);
                bar.gameObject.SetActive(true);
                break;
            }
        }
    }

}
