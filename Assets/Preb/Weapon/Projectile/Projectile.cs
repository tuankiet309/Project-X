using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ITeamInterface
{
    [SerializeField] float flyHight = 10f;
    [SerializeField] Rigidbody rb;
    [SerializeField] DamageComponent damageComponent;
    [SerializeField] ParticleSystem explosionVFX;
    ITeamInterface teamInterface;

    public void Lauch(GameObject Instigator, Vector3 Destination)
    {
        
        teamInterface = Instigator.GetComponent<ITeamInterface>();
        if (teamInterface != null) 
        {
            damageComponent.SetTeamInterface(teamInterface);
        }
        float gravity = Physics2D.gravity.magnitude;
        float halfFlightTime = Mathf.Sqrt( (flyHight * 2) / gravity );
        

        Vector3 DestinationVec = Destination - transform.position;
        DestinationVec.y = 0;
        float horizontalDistance = DestinationVec.magnitude;

        float upSpeed = halfFlightTime * gravity;
        float fwdSpeed = horizontalDistance / (2*halfFlightTime);

        Vector3 flightVel = Vector3.up * upSpeed + DestinationVec.normalized * fwdSpeed;
        rb.AddForce( flightVel,ForceMode.VelocityChange );

    }

    private void OnTriggerEnter(Collider other)
    {
        if(teamInterface.GetRalationToward(other.gameObject) !=ETeamRalation.Friendly)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Vector3 spawnPos = transform.position;

        Instantiate(explosionVFX, spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
