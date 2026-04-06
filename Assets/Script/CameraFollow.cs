using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float smoothTime = 0.3f;
    
    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;
        
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );
    }
}
