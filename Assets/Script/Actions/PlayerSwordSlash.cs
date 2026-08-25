using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSlash : MonoBehaviour
{
    [SerializeField] SwordSlashEffect slashEffect;
    [SerializeField, Range(0f, 1f)] float showSlashTime = 0.5f;

    [Space]
    [SerializeField, Range(-1f, 1f)] float offset = 0f;
    
    // Awake
    PlayerMove move;
    
    
    // === Life Cycle === //
    void Awake()
    {
        move = GetComponent<PlayerMove>();
    }

    void Start()
    {
        CreateSwordSlash();
    }


    // === Self Method === //
    IEnumerator CountDownCoroutine(float times, Action method)
    {
        yield return new WaitForSeconds(times);
        
        method?.Invoke();
    }
    
    void CreateSwordSlash()
    {
        if (GetComponentInChildren<SwordSlashEffect>() is null)
            slashEffect = Instantiate(slashEffect, transform);
        
        slashEffect.gameObject.SetActive(false);
    }
    
    
    // === API === //
    public void Effect(float displacement, float range)
    {
        slashEffect.Show(move.FacingDirection, displacement, range, offset);
        StartCoroutine(CountDownCoroutine(showSlashTime, slashEffect.Hide));
    }
}
