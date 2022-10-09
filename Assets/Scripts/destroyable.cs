using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyable : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
