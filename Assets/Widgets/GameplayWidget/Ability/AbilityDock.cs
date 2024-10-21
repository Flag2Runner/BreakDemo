using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityDock : Widget, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AbilityWidget abilityWidgetPrefab;
    [SerializeField] private RectTransform rootPanel;
    [SerializeField] private float scaleRange = 150f;
    
    private List<AbilityWidget> _abilityWidgets = new List<AbilityWidget>();
    
    private AbilitySystemComponent _abilitySystemComponent;
    
    private PointerEventData _touchData;

    private void Update()
    {
        if (_touchData != null)
        {
            ArrayScale();
        }
        else
        {
            ResetScale();
        }
    }

    private void ResetScale()
    {
        foreach (AbilityWidget abilityWidget in _abilityWidgets)
        {
            abilityWidget.SetScaleAmt(0, rootPanel);
        }
    }

    private void ArrayScale()
    {
        float touchYPos = _touchData.position.y;
        foreach (AbilityWidget abilityWidget in _abilityWidgets)
        {
            float widgetYPos = abilityWidget.transform.position.y;
            float distanceToTouch = Mathf.Abs(widgetYPos - touchYPos);
            if (distanceToTouch > scaleRange)
            {
                abilityWidget.SetScaleAmt(0, rootPanel);
                continue;
            }

            float scaleAmt = (scaleRange - distanceToTouch) / scaleRange;
            abilityWidget.SetScaleAmt(scaleAmt, rootPanel);
        }
    }

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        _abilitySystemComponent = newOwner.GetComponent<AbilitySystemComponent>();
        if (_abilitySystemComponent)
        {
            _abilitySystemComponent.onAbilityGiven += AbilityGiven;
        }
    }

    private void AbilityGiven(Ability newAbility)
    {
        AbilityWidget newAbilityWidget = Instantiate(abilityWidgetPrefab, rootPanel);
        newAbilityWidget.Init(newAbility);
        _abilityWidgets.Add(newAbilityWidget);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AcivateAbilityUnderTouch();
        _touchData = null;
    }

    private void AcivateAbilityUnderTouch()
    {
        List<RaycastResult> raycastResults= new List<RaycastResult>();
        EventSystem.current.RaycastAll(_touchData, raycastResults);

        foreach (RaycastResult raycastResult in raycastResults)
        {
            AbilityWidget abilityWidget = raycastResult.gameObject.GetComponent<AbilityWidget>();
            if (abilityWidget != null)
            {
                abilityWidget.CastAbility();
                return;
            }
        }
    }
}
