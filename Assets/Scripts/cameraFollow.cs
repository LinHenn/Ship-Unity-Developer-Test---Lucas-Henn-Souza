using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeReference] private Transform player;

    // x � 6
    // y � 9
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    private void Start()
    {
        player = Boat.instance.gameObject.transform;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
        Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
        Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }
}
