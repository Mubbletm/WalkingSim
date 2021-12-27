using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadLookAtPlayer : MonoBehaviour
{
    public Vector3 rotationOffset;
    
    private GameObject player;
    private Transform cameraTransform;

    void Start()
    {
        player = GameObject.Find("Player");
        cameraTransform = player.transform.GetChild(0);
    }

    void Update()
    {
        Vector3 rotation = Quaternion.LookRotation(cameraTransform.position - transform.position).eulerAngles;
        rotation.x = 0;
        rotation += rotationOffset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
