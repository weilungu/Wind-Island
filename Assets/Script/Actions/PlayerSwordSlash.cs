using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSlash : MonoBehaviour
{
    [SerializeField] SwordSlashEffect swordSlash;
    [SerializeField, Range(0f, 1f)] float showSlashTime = 0.5f;
    
    
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

    void Update()
    {
        print($"dir {move.FacingDirection}");
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
            swordSlash = Instantiate(swordSlash, transform);
        
        swordSlash.gameObject.SetActive(false);
    }
    
    
    // === Self API === //
    public void Slash()
    {
        swordSlash.Show(move.FacingDirection);
        StartCoroutine(CountDownCoroutine(showSlashTime, swordSlash.Hide));
    }
}
