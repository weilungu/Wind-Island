using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackController
{
    [SerializeField] private bool hasPlayerInFront;
    public bool HasPlayerInFront => hasPlayerInFront;
    
    
    public bool CheckPlayerInFront()
    {
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            GetAttackOrigin(lastDirection),
            data.hitboxSize, 0,
            hitResults, targetLayers);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitResults[i] is null) continue;
            if (hitResults[i].GetComponent<PlayerController_orig>() is not null)
            {
                hasPlayerInFront = true;
                return true;
            }
        }

        hasPlayerInFront = false;
        return false;
    }
}
