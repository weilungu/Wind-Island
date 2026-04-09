using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    
    Transform target = null;
    bool isChasing = false;
    
    Vector2 faceDir = Vector2.zero;
    
    [SerializeField] float speed = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform t)
    {
        target = t;
        isChasing = true;
    }
    public void ClearTarget()
    {
        target = null;
        isChasing = false;
    }

    void FixedUpdate()
    {
        if (isChasing && !target.Equals(null))
        {
            print($"Player is in range");
            Chase();
        }
    }

    void Chase()
    {
        faceDir = (target.position - transform.position).normalized;
        Vector2 velocity = faceDir * speed;
        
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
}
