using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent; //Thành phần Health
    Animator animator; //Animator
    Perception_Component perceeption_Component;
    GameObject target;
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>(); //Gán
        animator = GetComponent<Animator>(); //Gán
        perceeption_Component = GetComponent<Perception_Component>();
        if(healthComponent != null ) //nếu có
        {
            healthComponent.onHealthEmpty += StartDeath; //Gọi hàm StartDeath mỗi khi event onHealthEmpty đc invoke
            healthComponent.onTakeDamamge += TakenDamage; //Gọi hàm TakenDamage mỗi khi event onTakeDamamge đc invoke
        }
        perceeption_Component.onPerceptionTargetChanged += TargetChange;
    }

    private void TargetChange(GameObject target, bool sense)
    {
        if(sense)
        {
            this.target = target;  
        }
        else
        {
            this.target = null;
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator) 
    {
        
    }

    private void StartDeath()
    {
        TriggerDeathAnimation(); //bắt đầu animaton chết
        
    }
    public void OnDeathAnimationFinished() //Hủy gameObject khi chết// Gọi bằng animation event của animation dead
    {
        Destroy(gameObject);
    }
    private void TriggerDeathAnimation() 
    {
        if (animator != null)
        {
            animator.SetTrigger("isDead");//trigger để bắt đầu animation dead
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if(target!=null)
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(drawTargetPos, 1f);
            Gizmos.DrawLine(transform.position + Vector3.up, target.transform.position);
        }
    }

}
