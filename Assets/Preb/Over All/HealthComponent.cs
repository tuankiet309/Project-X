using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthComponent : MonoBehaviour
    
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamamge(float health, float delta, float maxHealth);
    public delegate void OnHealthEmpty( );

    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamamge onTakeDamamge;
    public event OnHealthEmpty onHealthEmpty;
    public void ChangeHealth( float amount)
    {
        if (amount == 0 || health==0)
            return;
        health += amount;
        if(amount<0)
        {
            onTakeDamamge?.Invoke(health,amount,maxHealth);
        }
        onHealthChange?.Invoke(health,amount,maxHealth);
        if(health<=0)
        {
            onHealthEmpty?.Invoke();
        }
        Debug.Log($"{gameObject} taking Damage {amount}, health now is {health}");
    }
}
