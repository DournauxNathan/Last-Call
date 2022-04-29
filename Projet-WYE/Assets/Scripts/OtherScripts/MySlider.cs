using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MySlider : Slider
{
    public UnityEvent<float,float> onValueChangedWithOldValue;
    public float oldValue;

    protected override void Set(float input, bool sendCallback = true)
    {
        oldValue = m_Value;
        base.Set(input, sendCallback);
        onValueChangedWithOldValue.Invoke(oldValue, m_Value);
    }

}
