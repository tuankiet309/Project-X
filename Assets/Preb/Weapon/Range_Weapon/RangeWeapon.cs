using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    AimComponent aimComponent;
    [SerializeField] ParticleSystem bulletFX;
    private void Start()
    {
        aimComponent = GetComponent<AimComponent>();
    }
    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget(out Vector3 aimDir);
        DamageOnGameObject(target, amount);
        bulletFX.transform.rotation = Quaternion.LookRotation(aimDir);
        bulletFX.Emit(bulletFX.emission.GetBurst(0).maxCount);
    }


}
