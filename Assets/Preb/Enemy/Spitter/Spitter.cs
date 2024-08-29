using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform lauchPoint;
    

    Vector3 Destination;
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("attack");
        Destination = target.transform.position;
    }
    public void Shoot()
    {
        Projectile newProjectile = Instantiate(projectilePrefab,lauchPoint.position,lauchPoint.rotation);
        newProjectile.Lauch(gameObject, Destination);
    }
}
