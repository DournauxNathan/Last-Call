using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyObject : MonoBehaviour
{
    public Transform[] Floaters;
    public float UnderWaterDrag = 3f;
    public float UnderWaterAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;
    public float FloatingPower = 15f;
     OceanManager oceanManager;

    Rigidbody Rb;
    bool Underwater;
    int FloatersUnderWater;
    // Start is called before the first frame update
    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
        oceanManager = FindObjectOfType<OceanManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FloatersUnderWater = 0;
        for (int i = 0; i < Floaters.Length; i++)
        {
            float diff = Floaters[i].position.y - oceanManager.WaterHeightAtPosition(Floaters[i].position);
            if (diff < 0)
            {
                Rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(diff), Floaters[i].position, ForceMode.Force);
                FloatersUnderWater += 1;
                if (!Underwater)
                {
                    Underwater = true;
                    SwitchState(true);
                }
            }
        }
        if (Underwater && FloatersUnderWater == 0)
        {
            Underwater = false;
            SwitchState(false);
        }
    }
    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            Rb.drag = UnderWaterDrag;
            Rb.angularDrag = UnderWaterAngularDrag;
        }
        else
        {
            Rb.drag = AirDrag;
            Rb.angularDrag = AirAngularDrag;
        }
    }
}

