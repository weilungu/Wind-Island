using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashEffect : MonoBehaviour
{
    
    
    // === Method === //
    void SetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void SetPosition(Vector3 direction, float distance)
    {
        transform.position =
            transform.parent.position + (direction * distance);
    }
    
    
    // === API ===
    public void Show(Vector2 direction, float displacement, float range, float offset = 0f)
    {
        SetPosition(direction, displacement + range + offset);
        SetRotation(direction);
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
