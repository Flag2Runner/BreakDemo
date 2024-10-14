using UnityEngine;

public class GameplayWidget : Widget
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;
    [SerializeField] private CanvasGroup gameplayControlCanvasGroup;
    
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
}
