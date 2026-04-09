using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform target;
    bool isChasing = false;
    
    public void SetTarget(Transform t)
    {
        target = t;
        isChasing = true;
    }
}
