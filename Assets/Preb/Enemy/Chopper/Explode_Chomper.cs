using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_Chomper : Chomper
{
    [SerializeField] ParticleSystem explode;
    protected override void Dead()
    {
        Instantiate(explode,transform.position + Vector3.up,transform.rotation);
    }
}
