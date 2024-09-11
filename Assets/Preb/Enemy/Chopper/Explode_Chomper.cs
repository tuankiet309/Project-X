using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_Chomper : Chomper,IExplodeAble
{
    [SerializeField] ParticleSystem explode;
    
    
    public void Explode()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(explode, transform.position + Vector3.up, transform.rotation);
    }

    protected override void Dead()
    {
        Explode();
    }
}
