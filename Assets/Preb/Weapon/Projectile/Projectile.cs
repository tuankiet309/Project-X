using UnityEngine;

public abstract class Projectile : MonoBehaviour, IExplodeAble
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected DamageComponent damageComponent;
    [SerializeField] protected ParticleSystem explosionVFX;
    protected ITeamInterface teamInterface;
    protected Vector3 offset;
    public virtual void Explode()
    {
        Vector3 spawnPos = transform.position + offset;
        Instantiate(explosionVFX, spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
    public virtual void Launch(GameObject instigator, Vector3 destination)
    {
        teamInterface = instigator.GetComponent<ITeamInterface>();
        if (teamInterface != null)
        {
            damageComponent.SetTeamInterface(teamInterface);
        }

        //How to launch implement in child class
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ETeamRalation relation = teamInterface.GetRalationToward(other.gameObject);
        if (relation != ETeamRalation.Friendly)
        {
            offset = relation == ETeamRalation.Enemy ? Vector3.down : Vector3.zero;
            Explode();
        }
    }
}