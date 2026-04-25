using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Debug")] 
    [SerializeField] private int currCombo;
    [SerializeField] private bool isAttacking; // 攻擊動作進行中（動畫未結束）
    // [SerializeField] bool hasPlayerInFront;

    private float nextAttackTime;
    private float lastAttackTime;
    private float comboCooldownEndTime;

    protected Vector2 lastDirection = Vector2.right;

    [Header("Field Instance")] 
    [SerializeField] private Transform attackPoint;
    [SerializeField] protected LayerMask targetLayers;

    [Header("Attack Data")]
    [SerializeField] protected AttackData data;

    protected Collider2D[] hitResults = new Collider2D[32];

    // ── 公開查詢 ──────────────────────────────────────────────────────────
    public bool canAttack => Time.time >= nextAttackTime && Time.time >= comboCooldownEndTime;
    public bool IsAttacking => isAttacking;
    // public bool HasPlayerInFront => hasPlayerInFront;


    // ── 公開 API ──────────────────────────────────────────────────────────
    public bool IsTargetInRange(Vector2 targetPos)
    {
        return Vector2.Distance((Vector2)transform.position, targetPos) <= data.attackRange;
    }
    
    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
            lastDirection = normalized;
    }

    // public bool CheckPlayerInFront()
    // {
    //     int hitCount = Physics2D.OverlapBoxNonAlloc(
    //         GetAttackOrigin(lastDirection),
    //         data.hitboxSize, 0,
    //         hitResults, targetLayers);
    //
    //     for (int i = 0; i < hitCount; i++)
    //     {
    //         if (hitResults[i] is null) continue;
    //         if (hitResults[i].GetComponent<PlayerController>() is not null)
    //         {
    //             hasPlayerInFront = true;
    //             return true;
    //         }
    //     }
    //
    //     hasPlayerInFront = false;
    //     return false;
    // }

    public bool TryAttack(Vector2 direction)
    {
        if (!canAttack) return false;

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
            if (hitResults[i] is null) continue;

            Health hp = hitResults[i].GetComponent<Health>();
            Posture posture = hitResults[i].GetComponent<Posture>();

            if (hp is not null)
            {
                hp.TakeDamage(data.damage);
            }

            if (posture is not null)
            {
                posture.TakePostureDamage(data.posture);
            }
        }
    }

    protected Vector2 GetAttackOrigin(Vector2 attackDir)
    {
        Vector2 originBase = attackPoint is not null
            ? (Vector2)attackPoint.position
            : (Vector2)transform.position;

        return originBase + attackDir * data.attackRange;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint is null || data is null) return;
        Gizmos.DrawWireCube(
            GetAttackOrigin(lastDirection),
            new Vector3(data.hitboxSize.x, data.hitboxSize.y, 1f));
    }
}