 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform Muzzle;
    [SerializeField] float aimRange = 1000f;
    [SerializeField] LayerMask aimLayerMasked;
    
    

    public GameObject GetAimTarget(out Vector3 aimDir)
    {
        Vector3 aimPos = Muzzle.position;
        aimDir = GetAimDir();

        if(Physics.Raycast(aimPos,aimDir, out RaycastHit hitInfo,aimRange, aimLayerMasked))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }
    Vector3 GetAimDir()
    {
        Vector3 muzzleDir = Muzzle.forward;
        return new Vector3(muzzleDir.x,0,muzzleDir.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Muzzle.position, Muzzle.position + GetAimDir() * aimRange);
    }
}
