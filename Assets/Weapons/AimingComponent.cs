using System;
using UnityEngine;
using UnityEngine.Serialization;

public struct AimResult
{
    public GameObject target;
    public Vector3 aimStart;
    public Vector3 aimDir;

    public AimResult(GameObject inTarget, Vector3 inAimStart, Vector3 inAimDir)
    {
        target = inTarget;
        aimStart = inAimStart;
        aimDir = inAimDir;
    }
}

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange = 10f;
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private bool bOverrideLocation = true;
    [SerializeField] private bool bFlattenAim = true;
    [SerializeField] private float heightOverride = 1.5f;
    [SerializeField] private float fwdOffset = 0.5f;
    
    public AimResult GetAimResult(Transform aimTransform = null)
    {
        Vector3 aimStart = muzzle.position;
        Vector3 aimDir = muzzle.forward;
        
        if (aimTransform)
        {
            aimStart = aimTransform.position;
            aimDir = aimTransform.forward;
        }

        if (bFlattenAim)
        {
            aimDir.y = 0;
            aimDir.Normalize();
        }
        
        if (bOverrideLocation)
        {
            aimStart.y = heightOverride;
            aimStart += aimDir * fwdOffset;
        }

        GameObject target = null;
        if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            target = hitInfo.collider.gameObject;
        }

        return new AimResult(target, aimStart, aimDir);
    }
}
