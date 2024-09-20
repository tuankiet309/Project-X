using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IRewardListener
    
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth); //delegate khi máu thay đổi
    public delegate void OnTakeDamamge(float health, float delta, float maxHealth, GameObject Instigator); //delegate khi nhận sát thương
    public delegate void OnHealthEmpty( GameObject killer); //delegate khi hết máuu

    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;

    public event OnHealthChange onHealthChange; //Các event tương ứng
    public event OnTakeDamamge onTakeDamamge; //
    public event OnHealthEmpty onHealthEmpty; //

    [Header("Audio")]
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;
    [SerializeField] float volume;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeHealth( float amount, GameObject Instigator) //Khi máu thay đổi
    {
        if (amount == 0 || health==0) //hết máu hoặc ko damage nhận vào
            return;
        health += amount; //thay đổi máu bằng damage
        health = Mathf.Clamp(health, 0,maxHealth);
        if(amount<0) //Nếu nhận sát thương
        {
            onTakeDamamge?.Invoke(health,amount,maxHealth,Instigator); //Chạy các hàm đăng ký với event này
            Vector3 loc = transform.position;
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(hit);
            }
        }
        onHealthChange?.Invoke(health,amount,maxHealth); //Chạy các hàm đăng ký với event này
        if (health<=0)
        {
            health = 0;
            onHealthEmpty?.Invoke(Instigator); //Chạy các hàm đăng ký với event này
            Vector3 loc = transform.position;
            GameStatic.PlayAudioAtLoc(death, loc, volume);
        }
        Debug.Log($"{gameObject} taking Damage {amount}, health now is {health}"); // đừng quan tâm
    }
    public void BroadCastHealthValueImmediately()
    {
        onHealthChange?.Invoke(health, 0, maxHealth);
    }

    public void Reward(Reward reward)
    {
        health = Mathf.Clamp(health + reward.healthReward, 0, maxHealth);
        onHealthChange?.Invoke(health,reward.healthReward, maxHealth); ;
    }
}
