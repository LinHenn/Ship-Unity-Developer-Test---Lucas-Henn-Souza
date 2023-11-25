using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    public CharacterLifeData characterLifeData;
    public event Action<int> OnLifeChanged;
    public event Action onDie;
    private int life;
    public int Life
    {
        get { return life; }
        set {
            if (life < 0) value = 0;
            life = value;
            OnLifeChanged?.Invoke(life);
            setImgBoat();
            if (life == 0) 
            {
                onDie?.Invoke(); 
            }
        }
    }

    private DateTime lastTimeDamage;

    [SerializeField] private SpriteRenderer boatSprite;
    [SerializeField] private Sprite[] boats;



    void Start()
    {

        OnGame();
    }

    private void setImgBoat()
    {
        float result = (float)Life / (float)characterLifeData.fullLife;

        if(result > 0.7f) boatSprite.sprite = boats[3];
        else if (result > 0.3f) boatSprite.sprite = boats[2];
        else if (result > 0f) boatSprite.sprite = boats[1];
        else boatSprite.sprite = boats[0];
    }

    public void OnGame()
    {
        Life = 3;
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
