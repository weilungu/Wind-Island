using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] int attackDamage = 10;
    
    [Space]
    [Header("Attack Detection")]
    [SerializeField] Transform attackPoint;
    [SerializeField, Tooltip("Distance of Attack Point with Player \n(Need Dynamic Adjustment)")]
    float displacement;
    
    [Space]
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;
    
    
    [Header("Attack Times")]
    [SerializeField] int maxAttackSteps = 3;
    
    [SerializeField, Range(0f, 5f), Tooltip("攻擊達最高次數, 冷卻數秒並重置目前次數")]
    float cooldown = 1f;
    
    [SerializeField, Range(0f, 5f), Tooltip("未達最高次數 && 超過時間, 重置目前次數")]
    float timeout = 1f;
    
    
    [Header("Sword Slash")]
    [SerializeField] SwordSlash swordSlash;
    [SerializeField, Range(0f, 1f)] float showSlashTime = 0.5f;
    
    
    [Header("Debug Show")]
    [SerializeField] int currAttackStep = 0;
    
    [Space]
    [SerializeField] Color rangeColor = Color.red;

    
    // Awake Values
    PlayerMove move;
    
    // Private Values
    Coroutine timeoutCoroutine;
    Coroutine cooldownCoroutine;
    
    Collider2D[] hitEnemies = new Collider2D[1];
    
    
    // === Life Cycle ===
    void Awake()
    {
        move = GetComponent<PlayerMove>();
    }
    void Start()
    {
        SetAttackPoint(Vector2.right);
        CreateSwordSlash();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint is null) return;
        
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    

    // === Self Method === //
    void ResetCurrentStep() => currAttackStep = 0;

    void CreateSwordSlash()
    {
        if (GetComponentInChildren<SwordSlash>() is null)
            swordSlash = Instantiate(swordSlash, transform);
        
        swordSlash.gameObject.SetActive(false);
    }
    
    
    // Coroutine Methods
    IEnumerator CountDownCoroutine(float timesType, Action method)
    {
        yield return new WaitForSeconds(timesType);
        
        method?.Invoke();
    }

    void ResetCoroutines(ref Coroutine routine, float timesType)
    {
        if (timeoutCoroutine is not null)
            StopCoroutine(timeoutCoroutine);
        
        if (cooldownCoroutine is not null)
            StopCoroutine(cooldownCoroutine);
        
        
        if (routine is not null)
            StopCoroutine(routine);
        
        routine = StartCoroutine(CountDownCoroutine(timesType, ResetCurrentStep));
    }

    
    // === Self API === //
    public void Attack()
    {
        int enemiesNum = Physics2D.OverlapCircleNonAlloc(
            point: attackPoint.position,
            radius: attackRange,
            results: hitEnemies,
            layerMask: enemyLayers
        );

        swordSlash.Show(move.facingDirection);
        StartCoroutine(CountDownCoroutine(showSlashTime, swordSlash.Hide));
        
        if (enemiesNum > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<New_Enemy>().TakeDamage(attackDamage);
                print($"We hit {enemy.name}");
            }
        }
    }

    public void AdvanceStep()
    {
        currAttackStep++;

        if (!CanAttack)
        {
            ResetCoroutines(ref cooldownCoroutine, cooldown);
            return;
        }
        ResetCoroutines(ref timeoutCoroutine, timeout);
    }

    public void SetAttackPoint(Vector2 direction)
        => attackPoint.localPosition = direction * displacement;
    
    
    public bool CanAttack => currAttackStep < maxAttackSteps;
}
