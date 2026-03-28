using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] public bool canDash;
    [SerializeField] public bool isDashing;
    
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashCooldown;
    
    
    [Header("Instance")]
    [SerializeField] TrailRenderer trail;
    [SerializeField] Rigidbody2D rb;
    
    public IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = direction.normalized * dashSpeed;
        
        trail.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trail.emitting = false;
        
        rb.velocity = Vector2.zero;
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        
        canDash = true;
    }
}
