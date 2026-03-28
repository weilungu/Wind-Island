using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] public bool canDash;
    [SerializeField] public bool isDashing;

    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;


    [Header("Instance")] [SerializeField] Rigidbody2D rb;
    [SerializeField] InputController inp;

    public IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = direction.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public void Dash()
    {
        if (isDashing)
        {
            return;
        }

        if (canDash)
        {
            Vector2 moveDir = inp.GetMoveInput();
            print("Dashing");
            StartCoroutine(DashRoutine(moveDir));
        }
    }
}