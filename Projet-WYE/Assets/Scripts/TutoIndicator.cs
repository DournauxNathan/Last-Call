using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoIndicator : MonoBehaviour
{
    [Header("Param")]
    [SerializeField] [Range(1f, 10f)] private float decaySpeed = 7f;
    [SerializeField] private Light indicatorLight;
    private bool StateLight = true;
    private float current;

    // Start is called before the first frame update
    void Start()
    {
        current = decaySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (StateLight && indicatorLight.intensity>0)
        {
            indicatorLight.intensity = DecreaseLight();
        }
        if (StateLight && indicatorLight.intensity == 0)
        {
            StateLight = false;
        }

        if (!StateLight && indicatorLight.intensity<1)
        {
            indicatorLight.intensity = IncreaseLight();
        }
        if (!StateLight && indicatorLight.intensity == 1)
        {
            StateLight = true;
        }
    }

    private float IncreaseLight()
    {
        current += Time.deltaTime;
        float percentComplete = current / decaySpeed;
        return Mathf.Clamp01(percentComplete);
    }

    private float DecreaseLight()
    {
        current -= Time.deltaTime;
        float percentComplete = current * decaySpeed;
        return Mathf.Clamp01(percentComplete);
    }

}
