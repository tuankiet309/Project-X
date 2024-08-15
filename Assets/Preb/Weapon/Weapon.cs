using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] protected float amount = -2f;

    public abstract void Attack() ;
    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }
    public GameObject Owner {  get; private set; }

    public void Init(GameObject owner)
    { 
        this.Owner = owner;
        Unequip();
    }
    public void Equip()
    {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
    }
    public void Unequip()
    {
        gameObject.SetActive(false);
    }

    protected void DamageOnGameObject(GameObject gameObject,float amount)
    {
        HealthComponent healthComponent = gameObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(-amount);
        }

    }

}
