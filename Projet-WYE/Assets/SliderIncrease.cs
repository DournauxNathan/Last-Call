using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderIncrease : MonoBehaviour
{
    public float speed;
    public Slider _slider;

    private void Start()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            speed = 0.0025f;
        }
        else if (MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            _slider.value = 0.75f;
            speed = 0.003f;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(IncreaseBySpeed());
    }

    IEnumerator IncreaseBySpeed()
    {
        while (true)
        {
            yield return null;

            _slider.value += Time.deltaTime * speed;
        }
    }
}
