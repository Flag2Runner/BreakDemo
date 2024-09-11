using System;
using UnityEngine;

public class AimingComponent : MonoBehaviour
{
   [SerializeField] private Transform muzzle;
   [SerializeField] private float aimRange = 10f;
   [SerializeField] private LayerMask aimMask;
   [SerializeField] private bool bOveride = false;
   [SerializeField] private float heightOveride = 1.5f;
   [SerializeField] private float fwdOffset = 0.5f;
   // ToDO:Remove This
   private Vector3 _debugAimStart;
   private Vector3 _debugAimDir;
   public GameObject GetAimTarget(Transform aimTransform = null)
   {
      Vector3 aimStart = muzzle.position;
      Vector3 aimDir = muzzle.forward;
      if (aimTransform)
      {
         aimStart = aimTransform.position;
         aimDir = aimTransform.forward;
         bOveride = true;
      }
      else
      {
         bOveride = false;
      }

      if (bOveride)
      {
         aimStart.y = heightOveride;
         aimStart += aimDir * fwdOffset;
      }
      _debugAimStart = aimStart;
      _debugAimDir = aimDir;


      if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo,aimRange, aimMask))
      {
         return hitInfo.collider.gameObject;
      }
      return null;
   }

   private void OnDrawGizmos()
   {
      Gizmos.DrawLine(_debugAimStart ,_debugAimStart + _debugAimDir * aimRange);
   }
}
