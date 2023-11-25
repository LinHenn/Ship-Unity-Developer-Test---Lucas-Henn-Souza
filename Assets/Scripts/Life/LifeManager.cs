using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private CharacterLifeData characterLifeData;
    public event Action<int> OnLifeChanged;
    public event Action onDie;
    private int life;
    public int Life
    {
        get { return life; }
        set {
            if (life < 0) value = 0;
            life = value;
            //Debug.Log("Life: " + life);
            OnLifeChanged?.Invoke(life);
            if (life >= 0) boatSprite.sprite = boats[life];
            if (life == 0) 
            {
                onDie?.Invoke(); 
            }
        }
    }

    private DateTime lastTimeDamage;

    [SerializeField] private SpriteRenderer boatSprite;
    [SerializeField] private Sprite[] boats;

    [SerializeField] private HealthBar barLife;


    void Start()
    {
        OnGame();
        /*
        var lifebar = Instantiate(barLife, transform.position, Quaternion.identity);
        LifeBar = lifebar.GetComponent<HealthBar>();
        LifeBar.setBoat(this);
        */
    }

    public void OnGame()
    {
        Life = characterLifeData.fullLife;
        gameObject.SetActive(true);

        barCtrl.instance.setBar(this);
    }


    public bool isFullLife()
    {
        return life == characterLifeData.fullLife;
    }


    public float GetLifeNormalized()
    {
        return (float)life / characterLifeData.fullLife;
    }


    public bool TakeDamage(int power)
    {
        if (!CanTakeDamage()) return false;
        this.Life -= power;
        lastTimeDamage = DateTime.UtcNow;
        return true;
    }


    private bool CanTakeDamage()
    {
        if (!characterLifeData.invulneraleOnDamage) return true;
        if(characterLifeData.timeBetweenDamage > 0)
        {
            TimeSpan timeSpan = DateTime.UtcNow - lastTimeDamage;
            return timeSpan.TotalSeconds > characterLifeData.timeBetweenDamage;
        }
        return true;
    }
}
