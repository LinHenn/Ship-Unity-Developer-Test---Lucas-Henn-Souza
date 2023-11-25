using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Char_", menuName ="ScriptableObject/Character")]
public class CharacterLifeData : ScriptableObject
{
    [Tooltip("Max character life")]
    public int fullLife;
    [Tooltip("Recovery time between")]
    public float timeBetweenDamage;
    [Tooltip("Make character invulnerable during an amout of time")]
    public bool invulneraleOnDamage = true;

    public float moveSpeed;

    public int damage;

}
