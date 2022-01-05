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
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        sqrShakeDetectionTreshold = Mathf.Pow(shakeDetectionThreshold, 2);
        pillBox = GetComponent<PillBox>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(HandPresence.Instance.GetDeviceAccelation().sqrMagnitude);

        Debug.Log(HandPresence.Instance.GetDeviceAccelation().sqrMagnitude >= sqrShakeDetectionTreshold
            && Time.unscaledDeltaTime >= timeSinceLastShake + minShakeInterval);
        
        //Using Hand Acceleration
        if (HandPresence.Instance.GetDeviceAccelation().sqrMagnitude >= sqrShakeDetectionTreshold 
            && Time.unscaledDeltaTime >= timeSinceLastShake + minShakeInterval)
        {
            pillBox.Shake();
            timeSinceLastShake = Time.unscaledDeltaTime;
        }*/

        //Debug.Log(rb.velocity.sqrMagnitude >= sqrShakeDetectionTreshold);

        //Using GO's Rigidbody Acceleration
        if (rb.velocity.sqrMagnitude >= sqrShakeDetectionTreshold
           /*&& Time.unscaledDeltaTime >= timeSinceLastShake + minShakeInterval*/)
        {
            pillBox.Shake();
            timeSinceLastShake = Time.unscaledDeltaTime;
        }
    }
}
