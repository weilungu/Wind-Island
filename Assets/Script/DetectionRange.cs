using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    GameObject target;
    
    bool isChasing = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player is in range");
            
            target = other.gameObject;
            isChasing = true;
        }
    }
}
