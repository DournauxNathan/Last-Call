using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeight = 7f;
    public float wavesFrenquency = 1f;
    public float wavesSpeed = 4f;
    public Transform ocean;
    public Renderer oceanRend;

    Material OceantMat;
    Texture2D wavesDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }
    void SetVariables()
    {
        OceantMat = oceanRend.sharedMaterial;
        wavesDisplacement = (Texture2D)OceantMat.GetTexture("_WavesDisplacement");
    }
    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y + wavesDisplacement.GetPixelBilinear(position.x * wavesFrenquency/100, position.z * wavesFrenquency/100 + Time.time * wavesSpeed).g * wavesHeight/100 * ocean.localScale.x;
    }
    private void OnValidate()
    {
        if (!OceantMat)
            SetVariables();

        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        OceantMat.SetFloat("_WavesFrequency", wavesFrenquency);
        OceantMat.SetFloat("_WavesSpeed", wavesSpeed);
        OceantMat.SetFloat("_WavesHeight", wavesHeight);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
