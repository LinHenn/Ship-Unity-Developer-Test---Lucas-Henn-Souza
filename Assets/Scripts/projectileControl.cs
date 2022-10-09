using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileControl : MonoBehaviour
{
    public static projectileControl PC;
    public List<GameObject> projectiles;


    private void Start()
    {
        PC = this;
    }
    public GameObject setProjectile()
    {
        foreach(var projectil in projectiles)
        {
            if (!projectil.activeSelf) return projectil;
        }

        return null;
    }


}
