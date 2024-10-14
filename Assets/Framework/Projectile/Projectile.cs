using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, ITeamInterface
{
    Rigidbody _rigidbody;

    [SerializeField] float projectileThrowHeight = 3f;
    [SerializeField] float projctileBlowDamageRange = 4f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem blowUpVFX;

    public int TeamId
    {
        get;
        private set;
    }

    public GameObject Instigator
    {
        get;
        private set;
    }    

    public int GetTeamID()
    {
        return TeamId;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    public void Launch(Vector3 destination, GameObject instigator)
    {
        Instigator = instigator;
        ITeamInterface instigatorTeamInteface = instigator.GetComponent<ITeamInterface>();
        TeamId = instigatorTeamInteface.GetTeamID();

        float gravity = Physics.gravity.magnitude;
        float travelHalfTime = Mathf.Sqrt(2f * projectileThrowHeight/gravity);
        float verticalSpeed = gravity * travelHalfTime;

        Vector3 destinationVector = destination - transform.position;
        destinationVector.y = 0f;
        float horizontalSpeed = destinationVector.magnitude / (travelHalfTime * 2f); 
        Vector3 launchVelocity = verticalSpeed * Vector3.up + destinationVector.normalized * horizontalSpeed;

        _rigidbody.AddForce(launchVelocity, ForceMode.VelocityChange);
     }

    private bool ShouldBlowup(GameObject hitObject)
    {
        if (((ITeamInterface)this).GetTeamAttitudeTowards(hitObject) == TeamAttitude.Friendly)
            return false;

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ShouldBlowup(other.gameObject))
        {
            Blowup();
        }
    }

    private void Blowup()
    {
        Collider[] collidersInDamageRange = Physics.OverlapSphere(transform.position, projctileBlowDamageRange);

        foreach(Collider col in collidersInDamageRange)
        {
            if (((ITeamInterface)this).GetTeamAttitudeTowards(col.gameObject) != TeamAttitude.Enemy)
                continue;

            HealthComponent healthComp = col.GetComponent<HealthComponent>();
            if (healthComp == null)
                continue;

            healthComp.ChangeHealth(-damage, Instigator);
        }

        if(blowUpVFX)
            Instantiate(blowUpVFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
