using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector2 minmaxXY;
    void LateUpdate()
    {
        Vector3 pos = target.position;
        pos.z = transform.position.z;
        pos.x = Math.Clamp(pos.x, -minmaxXY.x, minmaxXY.x);
        pos.y = Math.Clamp(pos.y, -minmaxXY.y, minmaxXY.y);
        transform.position = pos;
    }
}
