using UnityEngine;

public class Enemy_One : EnemyController
{
    [Header("Enemy One - Dash Attack")]
    [SerializeField] float dashTriggerRange = 5f; // 進入此距離開始 Dash

    [SerializeField] float attackRange = 1.2f; // 到達此距離才打
    [SerializeField] float attackCooldown = 2f; // 打完後冷卻再回 Chase

    float attackCooldownEndTime;

    // ── Chase：進入 Dash 觸發距離就切換 ─────────────────────────────────
    protected override void OnChase()
    {
        base.OnChase(); // 更新 faceDir 與 AttackDirection

        if (DistanceToTarget() <= dashTriggerRange)
        {
            TryStartDash();
        }
    }

    // ── Dash：衝向 Player，到達攻擊範圍就停下來打 ────────────────────────
    protected override void OnDash()
    {
        UpdateFaceDir(); // Dash 期間持續追蹤方向

        if (DistanceToTarget() <= attackRange)
        {
            dash.ForceStop();
            fsm.SetGameState(EnemyState.Attack);

            if (hasPlayerInFront) print("hasPlayerInFront");
            return;
        }

        // Dash 自然結束但還不夠近 → 繼續追
        if (!dash.IsDashing)
        {
            fsm.SetGameState(EnemyState.Chase);
        }
    }

    // ── Attack：攻擊一次後冷卻，回 Chase 繼續循環 ────────────────────────
    protected override void EnemyAttack()
    {
        base.EnemyAttack();

        if (Time.time < attackCooldownEndTime) return;

        attack.UpdateAttackDirection(faceDir);

        if (attack.TryAttack(faceDir))
        {
            attackCooldownEndTime = Time.time + attackCooldown;
            fsm.SetGameState(EnemyState.Chase);
        }
    }

    public override void ActionState()
    {
        base.ActionState();
        switch (fsm.enemyState)
        {
            case EnemyState.Attack:
                // target.
                break;
        }
    }

    // ── 工具方法 ──────────────────────────────────────────────────────────
    // protected void TryStartDash()
    // {
    //     UpdateFaceDir();
    //     if (dash.TryDash(faceDir))
    //     {
    //         fsm.SetGameState(EnemyState.Dash);
    //     }
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dashTriggerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}