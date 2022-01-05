using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShakeDetector : MonoBehaviour
{
    public float shakeDetectionThreshold;
    public float minShakeInterval;

    private float sqrShakeDetectionTreshold;
    private float timeSinceLastShake;
    private PillBox pillBox;

    // Start is called before the first frame update
    void Start()
    {
        sqrShakeDetectionTreshold = Mathf.Pow(shakeDetectionThreshold, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (HandPresence.Instance.GetDeviceAccelation().sqrMagnitude >= sqrShakeDetectionTreshold && Time.unscaledDeltaTime >= timeSinceLastShake + minShakeInterval)
        {
            timeSinceLastShake = Time.unscaledDeltaTime;
        }
    }
}
