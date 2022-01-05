using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBox : MonoBehaviour
{
    public int nPill;    
    public List<Pill> pills;

    [Header("Refs")]
    [SerializeField] private Transform opening = null;
    [SerializeField] private Transform pullingStock = null;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shakeFX;

    [SerializeField] private bool instantiatePills;

    private void Update()
    {
        if (instantiatePills)
        {
            instantiatePills = false;
            Shake();
        }
    }

    public void Shake()
    {
        for (int i = 0; i < nPill; i++)
        {
            var pill = FindAvailablePills();
        }
    }

    public Pill FindAvailablePills()
    {
        foreach (var pill in pills)
        {
            if (!pill.isInstiantiated)
            {
                pill.Activate(opening, pullingStock);
                return pill;
            }
        }
        Debug.LogError("There is not enough Pills in the pullingStock");
        return null;
    }

    public void UpdateBox()
    {

    }
}
