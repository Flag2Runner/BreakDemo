using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    [SerializeField] private RectTransform rootPanel;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private Color canCastColor = Color.white;
    [SerializeField] private Color cannotCastColor = Color.grey;
    [SerializeField] private float cooldownUpdateInterval = 0.05f;

    [SerializeField] private float scaleSize = 1.5f;
    [SerializeField] private float scaleOffset = 100f;
    [SerializeField] private float scaleRate = 20f;
    
    Vector3 _goalScale = Vector3.one;
    private Vector3 _goalLocalOffset = Vector3.zero;
    private RectTransform _dockRootPanel = null;
    
    private Ability _ability;

    /// <summary>
    /// Set the amount of scale and Takes the dockRootPanel
    /// </summary>
    /// <param name="amt"> ranges from 0 to 1, 0 means no scaling up, 1 means full scaling up </param>
    /// <param name="dockRootPanel">The RectTransform of the Ability Dock to have it move with the abilities </param>
    public void SetScaleAmt(float amt, RectTransform dockRootPanel)
    {
        _dockRootPanel = dockRootPanel;
        _goalScale = Vector3.one * (1 + (scaleSize - 1) * amt);
        _goalLocalOffset = Vector3.left * (amt * scaleOffset);
    }

    private void Update()
    {
        rootPanel.transform.localScale = Vector3.Lerp(rootPanel.transform.localScale, _goalScale, Time.deltaTime * scaleRate);
        rootPanel.transform.localPosition = Vector3.Lerp(rootPanel.transform.localPosition, _goalLocalOffset, Time.deltaTime * scaleRate);
        iconImage.color = _ability.CanCast() ? canCastColor : cannotCastColor;
        if (_dockRootPanel)
        {
            _dockRootPanel.transform.localScale = Vector3.Lerp(rootPanel.transform.localScale, _goalScale, Time.deltaTime * scaleRate);
        }

    }

    internal void CastAbility()
    {
        _ability.TryActiveateAbility();
    }

    public void Init(Ability newAbility)
    {
        _ability = newAbility;
        if(_ability)
            _ability.OnAblityCooldownStarted += StartCooldown;
        _ability.OnAbilityCanCastChanged += CanCastStateChanged;
        
        iconImage.sprite = _ability.GetAbilityIcon();
        CanCastStateChanged(_ability.CanCast());

    }

    private void CanCastStateChanged(bool bCanCast)
    {
        iconImage.color = _ability.CanCast() ? canCastColor : cannotCastColor;
    }

    private void StartCooldown(float cooldownDuration)
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration));
    }

    IEnumerator CooldownCoroutine(float cooldownDuration)
    {
        float cooldownCounter = cooldownDuration;
        while (cooldownCounter > 0)
        {
            cooldownCounter -= cooldownUpdateInterval;
            cooldownImage.fillAmount = cooldownCounter / cooldownDuration;
            yield return new WaitForSeconds(cooldownUpdateInterval);
        }

        cooldownImage.fillAmount = 0;
    }
}
