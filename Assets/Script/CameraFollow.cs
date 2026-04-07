using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float smoothTime = 0.3f;
    
    [Header("Border")]
    [SerializeField] Vector2 min;
    [SerializeField] Vector2 max;
    
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
        
        float clampedX = Mathf.Clamp(transform.position.x, min.x, max.x);
        float clampedY = Mathf.Clamp(transform.position.y, min.y, max.y);
        
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
