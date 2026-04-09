using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] EnemyController enemy;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetTarget(other.transform);
            print($"Player is in range: {other.transform}");
        }
    }
}
