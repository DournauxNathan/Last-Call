using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBox : MonoBehaviour
{
    [Tooltip("Number of pills contained in the box")]
    public int nPill;
    public int nPillToDisplay;
    public List<Pill> pills;

    [Header("Refs")]
    [SerializeField] private Transform opening = null;
    [SerializeField] private Transform pullingStock = null;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shakeFX;

    [SerializeField] private bool instantiatePills;

    private void Start()
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
                pill.Activate(pullingStock);
                return pill;
            }
        }
        Debug.LogError("There is not enough Pills in the pullingStock");
        return null;
    }

    public void Shake()
    {
        instantiatePills = true;

        foreach (var pill in pills)
        {
            if (instantiatePills && pill.isInstiantiated && !pill.hasMove)
            {
                pill.Move(opening);
                UpdatePillNumber();

                instantiatePills = false;
            }         
        }
    }

    public void UpdatePillNumber()
    {
        nPill -= nPillToDisplay;
    }
}
