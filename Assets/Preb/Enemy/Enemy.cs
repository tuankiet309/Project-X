using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,BehaviorTreeInterface,ITeamInterface
{
    HealthComponent healthComponent; //Thành phần Health
    Animator animator; //Animator
    Perception_Component perceeption_Component; //Thành phần tri giác
    Behavior_Tree behavior; //Cây hành vi
    MovementComponent movement_Component;
    Vector3 preLocation;
    [SerializeField] int TeamID = 2;

    public int GetTeamID()
    {
        return (int)TeamID;
    }

    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }
  
    protected virtual void Start()
    {
        movement_Component = GetComponent<MovementComponent>();
        healthComponent = GetComponent<HealthComponent>(); //Gán
        animator = GetComponent<Animator>(); //Gán
        perceeption_Component = GetComponent<Perception_Component>();
        behavior = GetComponent<Behavior_Tree>();
        if(healthComponent != null ) //nếu có
        {
            healthComponent.onHealthEmpty += StartDeath; //Gọi hàm StartDeath mỗi khi event onHealthEmpty đc invoke
            healthComponent.onTakeDamamge += TakenDamage; //Gọi hàm TakenDamage mỗi khi event onTakeDamamge đc invoke
        }
        perceeption_Component.onPerceptionTargetChanged += TargetChange; //Gàn hàm targetChange để gọi mỗi khi có thay đổi về hành target
    }
    

    private void TargetChange(GameObject target, bool sense)
    {
        if(sense)
        {
            behavior.Board.SetOrAddData("Target", target); 
        }
        else
        {
            behavior.Board.SetOrAddData("LastSeenLocation",target.transform.position);
            behavior.Board.RemoveData("Target");
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
    protected void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        Vector3 posDelta = transform.position - preLocation;
        float speed = posDelta.magnitude / Time.deltaTime;
        Animator.SetFloat("speed", speed);
        preLocation = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(behavior && behavior.Board.GetData("Target",out GameObject target))
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(drawTargetPos, 1f);
            Gizmos.DrawLine(transform.position + Vector3.up, target.transform.position);
        }
    }

    public void RotateToward(GameObject target, bool verticleAim = false)
    {
        Vector3 aimDir = target.transform.position - transform.position;
        aimDir.y = verticleAim ? aimDir.y : 0;
        aimDir = aimDir.normalized;
        movement_Component.RotateTowards(aimDir);
    }



    public virtual void AttackTarget(GameObject target)
    {
    }
}
