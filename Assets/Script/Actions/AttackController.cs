using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Debug")] [SerializeField] int currCombo;
    [SerializeField] bool isAttacking; // 攻擊動作進行中（動畫未結束）

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
    public bool canAttack => Time.time >= nextAttackTime && Time.time >= comboCooldownEndTime;

    /// <summary>攻擊動畫播放中（由 Animation Event 控制開關）。</summary>
    public bool IsAttacking => isAttacking;

    // ── 公開 API ──────────────────────────────────────────────────────────

    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
            lastDirection = normalized;
    }

    /// <summary>
    /// 嘗試發動攻擊；回傳 true 代表這幀確實打出了一擊。
    /// PlayerController 只在 Attack state 下呼叫此方法。
    /// </summary>
    public bool TryAttack(Vector2 direction)
    {
        if (!canAttack) return false;

        // Combo reset
        if (currCombo > 0 && Time.time - lastAttackTime > data.comboResetTime)
            currCombo = 0;

        currCombo++;
        lastAttackTime = Time.time;
        nextAttackTime = Time.time + data.attackRate;
        isAttacking = true;

        PerformAttack(direction);

        if (currCombo >= data.maxCombo)
        {
            currCombo = 0;
            comboCooldownEndTime = Time.time + data.comboCooldown;
        }

        return true;
    }

    // ── Animation Events（掛在攻擊動畫上）────────────────────────────────

    /// <summary>攻擊動畫結束時由 Animation Event 呼叫，通知狀態機可以離開 Attack state。</summary>
    public void OnAttackAnimEnd()
    {
        isAttacking = false;
    }

    // ── 工具方法 ──────────────────────────────────────────────────────────

    public bool TryGetPlayerInFront(out Transform player)
    {
        player = null;

        int hitCount = Physics2D.OverlapBoxNonAlloc(
            GetAttackOrigin(lastDirection),
            data.hitboxSize, 0,
            hitResults, targetLayers);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitResults[i] == null) continue;
            if (hitResults[i].CompareTag("Player") ||
                hitResults[i].GetComponent<PlayerController>() != null)
            {
                player = hitResults[i].transform;
                return true;
            }
        }

        return false;
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