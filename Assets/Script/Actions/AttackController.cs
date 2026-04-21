using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] int  currCombo;
    [SerializeField] bool isAttacking;    // 攻擊動作進行中（動畫未結束）
    [SerializeField] bool hasPlayerInFront;

    float nextAttackTime;
    float lastAttackTime;
    float comboCooldownEndTime;

    Vector2 lastDirection = Vector2.right;

    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayers;

    [Header("Attack Data")]
    [SerializeField] AttackData data;

    Collider2D[] hitResults = new Collider2D[32];

    // ── 公開查詢 ──────────────────────────────────────────────────────────

    /// <summary>攻擊冷卻結束且 Combo 冷卻結束，可以發動下一擊。</summary>
    public bool canAttack       => Time.time >= nextAttackTime && Time.time >= comboCooldownEndTime;

    /// <summary>攻擊動畫播放中（由 Animation Event 控制開關）。</summary>
    public bool IsAttacking     => isAttacking;

    /// <summary>攻擊判定範圍內偵測到 Player。每幀由 CheckPlayerInFront() 更新。</summary>
    public bool HasPlayerInFront => hasPlayerInFront;

    /// <summary>目標是否在 attackRange 的圓形範圍內（供 Enemy 停步用）。</summary>
    public bool IsTargetInRange(Vector2 targetPos)
    {
        return Vector2.Distance((Vector2)transform.position, targetPos) <= data.attackRange;
    }

    // ── 公開 API ──────────────────────────────────────────────────────────

    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
            lastDirection = normalized;
    }

    public bool CheckPlayerInFront()
    {
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            GetAttackOrigin(lastDirection),
            data.hitboxSize, 0,
            hitResults, targetLayers);

        for (int i = 0; i < hitCount; i++)
        {
            // if (hitResults[i] == null) continue;
            if (hitResults[i].GetComponent<PlayerController>() != null)
            {
                hasPlayerInFront = true;
                return true;
            }
        }

        hasPlayerInFront = false;
        return false;
    }
    
    public bool TryAttack(Vector2 direction)
    {
        if (!canAttack) return false;

        if (currCombo > 0 && Time.time - lastAttackTime > data.comboResetTime)
            currCombo = 0;

        currCombo++;
        lastAttackTime      = Time.time;
        nextAttackTime      = Time.time + data.attackRate;
        isAttacking         = true;

        PerformAttack(direction);

        if (currCombo >= data.maxCombo)
        {
            currCombo            = 0;
            comboCooldownEndTime = Time.time + data.comboCooldown;
        }

        return true;
    }

    // ── Animation Events ──────────────────────────────────────────────────
    
    public void OnAttackAnimEnd()
    {
        isAttacking = false;
    }

    // ── 私有方法 ──────────────────────────────────────────────────────────

    void PerformAttack(Vector2 direction)
    {
        Vector2 attackDir = direction.normalized;
        if (attackDir == Vector2.zero) attackDir = lastDirection;
        else lastDirection = attackDir;

        int hitCount = Physics2D.OverlapBoxNonAlloc(
            GetAttackOrigin(attackDir),
            data.hitboxSize, 0,
            hitResults, targetLayers);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitResults[i] == null) continue;
            var hp = hitResults[i].GetComponent<Health>();
            if (hp != null) hp.TakeDamage(data.damage);
        }
    }

    Vector2 GetAttackOrigin(Vector2 attackDir)
    {
        Vector2 originBase = attackPoint != null
            ? (Vector2)attackPoint.position
            : (Vector2)transform.position;

        return originBase + attackDir * data.attackRange;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null || data == null) return;
        Gizmos.DrawWireCube(
            GetAttackOrigin(lastDirection),
            new Vector3(data.hitboxSize.x, data.hitboxSize.y, 1f));
    }
}