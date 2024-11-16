using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPositionY : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posición inicial del objeto al comenzar
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        // Mantiene la posición Y inicial cada frame
        transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
    }
}