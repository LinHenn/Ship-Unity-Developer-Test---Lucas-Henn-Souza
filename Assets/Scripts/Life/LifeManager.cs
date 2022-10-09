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
            if (life < 0) return;
            life = value;
            //Debug.Log("Life: " + life);
            OnLifeChanged?.Invoke(life);
            if (life >= 0) boatSprite.sprite = boats[life];
            if (life == 0) onDie?.Invoke();
        }
    }

    private DateTime lastTimeDamage;

    [SerializeField] private SpriteRenderer boatSprite;
    [SerializeField] private Sprite[] boats;

    void Start()
    {
        Life = characterLifeData.fullLife;
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
