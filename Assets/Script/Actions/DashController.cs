using System.Collections;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] bool canDash   = true;
    [SerializeField] bool isDashing = false;
    
    public bool IsDashing => isDashing;

    // Instance
    private Rigidbody2D rb;
    private MoveController move;
    
    [SerializeField] private DashData data;

    // Dash 執行期間的狀態
    private Vector2 dashDir;
    private float elapsed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<MoveController>();
    }

    // ── 公開 API ──────────────────────────────────────────────────────────
    public bool TryDash(Vector2 direction)
    {
        if (isDashing || !canDash || direction.Equals(Vector2.zero)) return false;
        if (!CanDashInDirection(direction)) return false;

        dashDir = direction.normalized;
        elapsed = 0f;
        isDashing = true;
        canDash   = false;
        return true;
    }
    public void DashFixedUpdate()
    {
        if (!isDashing) return;

        if (elapsed < data.dashDuration)
        {
            Vector2 movement = dashDir * data.dashSpeed * Time.fixedDeltaTime;
            bool moved = TryDashMove(dashDir, movement);

            if (!moved)
            {
                // 撞牆無法繼續，提早結束
                EndDash();
                return;
            }

            elapsed += Time.fixedDeltaTime;
        }
        else
        {
            EndDash();
        }
    }
    
    public void ForceStop() => EndDash();

    // ── 私有方法 ──────────────────────────────────────────────────────────

    void EndDash()
    {
        isDashing = false;
        // isDashing = false 後 PlayerController 會在下一個 Update 偵測到並切換狀態
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(data.dashCooldown);
        canDash = true;
    }

    bool TryDashMove(Vector2 dir, Vector2 movement)
    {
        int hitCount = rb.Cast(dir, move.filter, move.hitResults, movement.magnitude + move.castDistance);

        if (hitCount == 0)
        {
            rb.MovePosition(rb.position + movement);
            return true;
        }

        // 盡量貼近障礙物
        float safeDistance = Mathf.Max(move.hitResults[0].distance - move.castDistance, 0f);
        if (safeDistance > 0f)
            rb.MovePosition(rb.position + dir * safeDistance);

        // 嘗試沿軸滑動
        return TrySlide(movement);
    }

    bool TrySlide(Vector2 movement)
    {
        bool slidX = false, slidY = false;

        Vector2 moveX = new Vector2(movement.x, 0f);
        if (moveX.magnitude > 0f)
        {
            int hitX = rb.Cast(moveX.normalized, move.filter, move.hitResults,
                               Mathf.Abs(movement.x) + move.castDistance);
            if (hitX == 0) { rb.MovePosition(rb.position + moveX); slidX = true; }
        }

        Vector2 moveY = new Vector2(0f, movement.y);
        if (moveY.magnitude > 0f)
        {
            int hitY = rb.Cast(moveY.normalized, move.filter, move.hitResults,
                               Mathf.Abs(movement.y) + move.castDistance);
            if (hitY == 0) { rb.MovePosition(rb.position + moveY); slidY = true; }
        }

        return slidX || slidY;
    }

    bool CanDashInDirection(Vector2 direction)
    {
        RaycastHit2D[] check = new RaycastHit2D[1];
        int hitCount = rb.Cast(direction.normalized, move.filter, check, move.castDistance * 3f);
        return hitCount == 0;
    }
}