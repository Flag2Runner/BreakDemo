using UnityEngine;
using UnityEngine.UIElements;

public class ValueGauge : Widget
{
    [SerializeField] private Slider slider;

    public void UpdateValue(float newValue, float newMaxValue)
    {
        if (newMaxValue == 0)
            return;

        slider.value = newValue / newMaxValue;
    }
}
