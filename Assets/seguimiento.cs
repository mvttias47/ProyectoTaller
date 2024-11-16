using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAvatar : MonoBehaviour
{
    public Transform avatar;    // Asigna aquí el transform del avatar
    private Vector3 initialOffset;

    void Start()
    {
        
        initialOffset = transform.position - avatar.position;
    }

    void Update()
    {
        
        transform.position = avatar.position + initialOffset;
        transform.rotation = avatar.rotation; 
    }
}