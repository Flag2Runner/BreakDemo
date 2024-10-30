using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWidget : Widget
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;
    [SerializeField] private CanvasGroup gameplayControlCanvasGroup;
    [SerializeField] private CanvasGroup shopGroup;
    
    private List<CanvasGroup> _widgetGroup = new();

    private CanvasGroup _currentActiveWidgetGroup;
    public static GameplayWidget Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        List<CanvasGroup> _canvasGroups = new();
        GetComponentsInChildren<CanvasGroup>(true, _canvasGroups);
        foreach(CanvasGroup _childCanvasGroup in _canvasGroups)
        {
            if(_childCanvasGroup.transform.parent == transform)
            {
                _widgetGroup.Add(_childCanvasGroup);
                SetGroupActive(_childCanvasGroup, false, false);
            }
        }

        SetCurrentActiveGroup(gameplayControlCanvasGroup);
    }

    private void SetCurrentActiveGroup(CanvasGroup canvasGroup)
    {
        if (_currentActiveWidgetGroup != null)
        {
            SetGroupActive(_currentActiveWidgetGroup, false, false);
        }
        _currentActiveWidgetGroup = canvasGroup;
        SetGroupActive(canvasGroup, true, true);
    }

    private void SetGroupActive(CanvasGroup childCanvasGroup, bool bInteractable, bool bVisible)
    {
        childCanvasGroup.interactable = bInteractable;
        childCanvasGroup.blocksRaycasts = bInteractable;
        childCanvasGroup.alpha = bVisible ? 1 : 0;
    }

    public JoyStick MoveStick
    {
        get => moveStick;
        private set => moveStick = value;
    }
    
    public JoyStick AimStick
    {
        get => aimStick;
        private set => aimStick = value;
    }

    public void SetGameplayControlEnabled(bool bIsEnabled)
    {
        gameplayControlCanvasGroup.blocksRaycasts = bIsEnabled;
        gameplayControlCanvasGroup.interactable = bIsEnabled;
    }

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        Widget[] allWidgets = GetComponentsInChildren<Widget>();
        foreach (Widget childWidget in allWidgets)
        {
            if(childWidget != this)
                childWidget.SetOwner(newOwner);
        }
    }

    public void SwitchToShop()
    {
        SetCurrentActiveGroup(shopGroup);
        GameplayStatics.SetGamePaused(true);
    }public void SwitchToGameplay()
    {
        SetCurrentActiveGroup(gameplayControlCanvasGroup);
        GameplayStatics.SetGamePaused(false);
    }
}
