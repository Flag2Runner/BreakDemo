using UnityEngine;

public class GamePlayWidget : Widget
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
