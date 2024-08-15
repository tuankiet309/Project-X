using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent;
    Animator animator;
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        if(healthComponent != null )
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamamge += TakenDamage;
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth)
    {
        
    }

    private void StartDeath()
    {
        TriggerDeathAnimation();
        
    }
    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }
    private void TriggerDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
