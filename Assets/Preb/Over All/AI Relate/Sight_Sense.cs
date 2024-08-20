using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight_Sense : Sense_Component
{
    [SerializeField] private float sight_Distance = 5f;
    [SerializeField] private float sight_Half_Angle = 30f;

    [SerializeField] private float eye_height = 5f;
    protected override bool IsStimuliSensable(Perception_Stimuli perception_Stimuli)
    {
        float distance = Vector3.Distance(transform.position, perception_Stimuli.transform.position);
        if (distance > sight_Distance)
        {
            return false;
        }
        Vector3 forwardDir = transform.forward;
        Vector3 stimuliDir = (perception_Stimuli.transform.position - transform.position).normalized;
        if (Vector3.Angle(stimuliDir, forwardDir) > sight_Half_Angle)
            return false;
        if (Physics.Raycast(transform.position + Vector3.up * eye_height, stimuliDir, out RaycastHit hitInfo, sight_Distance))
        {
            if (hitInfo.collider.gameObject != perception_Stimuli.gameObject)
            {
                return false;
            }
        }
        return true;
        
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + Vector3.up * eye_height;
        Gizmos.DrawWireSphere(drawCenter, sight_Distance);
        Vector3 leftLimitDir = Quaternion.AngleAxis(sight_Half_Angle, Vector3.up) * transform.forward;
        Vector3 rightLimitDir = Quaternion.AngleAxis(-sight_Half_Angle, Vector3.up) * transform.forward;

        Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDir * sight_Distance);
        Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDir * sight_Distance);
    }
    
}
