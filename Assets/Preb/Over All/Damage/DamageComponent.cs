using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent : MonoBehaviour
{
    [SerializeField] bool damageFriendly;
    [SerializeField] bool damageEnemy;
    [SerializeField] bool damageNeutral;
    ITeamInterface TeamInterface;
    public void SetTeamInterface(ITeamInterface @interface)
    {
        this.TeamInterface = @interface;
    }
    public bool shouldDamageThis(GameObject target)
    {
        if(TeamInterface == null)
            return false;
        ETeamRalation relation = TeamInterface.GetRalationToward(target);
        if(damageFriendly && relation == ETeamRalation.Friendly)
            return true;
        if(damageEnemy && relation == ETeamRalation.Enemy)
            return true;
        if (damageNeutral && relation == ETeamRalation.Neutral)
            return true;
        return false;
    }
}
