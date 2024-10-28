using System.Collections;
using System.Xml.Schema;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Fire")]
public class FireAbility : Ability
{
    [SerializeField] private float damageAmt = 20f;
    [SerializeField] private float damageDuration = 3f;

    [SerializeField] private float damageRadius = 20f;
    [SerializeField] private float damageScanDuration = 1f;

    [SerializeField] private GameObject scanVFX;
    [SerializeField] private GameObject burnVFX;
    
    [SerializeField] private Scaner scannerPrefab;
    

    protected override void ActivateAbility()
    {
        if (!CommitAbility())
        {
            return;
        }

        Scaner newScaner = Instantiate(scannerPrefab, OwnerAsc.gameObject.transform);
        newScaner.OnTargetDetected += TargetDetected;
        Instantiate(scanVFX, newScaner.ScanPivot);
        newScaner.StartScan(damageRadius, damageScanDuration);
    }

    private void TargetDetected(GameObject newTarget)
    {
        ITeamInterface targetTeamInterface = newTarget.GetComponent<ITeamInterface>();
        if(targetTeamInterface == null)
            return;
        if (targetTeamInterface.GetTeamAttitudeTowards(OwnerAsc.gameObject) != TeamAttitude.Enemy)
            return;

        HealthComponent targetHealthComp = newTarget.GetComponent<HealthComponent>();
        if(targetHealthComp == null)
            return;

        OwnerAsc.StartCoroutine(DamageCoroutine(targetHealthComp));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DamageCoroutine(HealthComponent targetHealthComponent)
    {
        float counter = 0f;
        float damageRate = damageAmt / damageDuration;
        GameObject newBurnVFX = Instantiate(burnVFX, targetHealthComponent.transform);
        while (counter < damageDuration && targetHealthComponent != null)
        {
            counter += Time.deltaTime;
            targetHealthComponent.ChangeHealth(-damageRate * Time.deltaTime, OwnerAsc.gameObject);
            yield return new WaitForEndOfFrame();
        }
        Destroy(newBurnVFX);
    }
}
