using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] public bool canDash;
    [SerializeField] public bool isDashing;
    
    [SerializeField] public float dashPower;
    [SerializeField] public float dashTime;
    [SerializeField] public float dashCooldown;
    
    
    [Header("Instance")]
    [SerializeField] TrailRenderer trail;
    [SerializeField] Rigidbody2D rb;
    
    public IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        
        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(dashCooldown);
        
        isDashing = false;
        canDash = true;
        
        yield break;
    }
}
