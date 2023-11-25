using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCtrl : MonoBehaviour
{
    [SerializeField] private List<GameObject> boats;


    public void newBoat()
    {
        foreach (var boat in boats)
        {
            if (!boat.activeSelf)
            {
                Vector2 randomPos = new Vector2(Random.Range(-18f, 18), Random.Range(-13, 15));
                boat.GetComponent<LifeManager>().OnGame();
                boat.transform.position = randomPos;
                break;
            }
        }
    }


}
