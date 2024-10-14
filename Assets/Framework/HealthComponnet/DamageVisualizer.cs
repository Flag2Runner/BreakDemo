using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    [SerializeField, ColorUsage(true, true)] Color damagedColor;
    [SerializeField] float damageColorDuration = 0.2f;
    [SerializeField] string damageColorMaterialParmName = "_EmissionOffset";
    Color origColor;
    CameraShaker _cameraShaker;
    private void Start()
    {
        HealthComponent healthComponet = GetComponent<HealthComponent>();
        if (healthComponet)
        {
            healthComponet.OnTakenDamage += TookDamage;
        }
        origColor = meshRenderer.material.GetColor(damageColorMaterialParmName);

        ICameraInterface cameraInterface = GetComponent<ICameraInterface>();
        if (cameraInterface != null)
        {
            _cameraShaker = cameraInterface.GetCamera().AddComponent<CameraShaker>();
        }
    }

    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        if(meshRenderer.material.GetColor(damageColorMaterialParmName) == origColor)
        {
            meshRenderer.material.SetColor(damageColorMaterialParmName, damagedColor);
            Invoke("ResetColor", damageColorDuration);
        }

        if(_cameraShaker)
        {
            _cameraShaker.StartShake();
        }
    }

    void ResetColor()
    {
        meshRenderer.material.SetColor(damageColorMaterialParmName, origColor); 
    }
}
