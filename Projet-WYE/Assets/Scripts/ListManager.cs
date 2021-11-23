using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ListManager : MonoBehaviour
{

    public List<GameObject> HoveredInteractors;
    public List<GameObject> LockedInteractors;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClearList()
    {
        HoveredInteractors.Clear();
    }

    public void PressToSubmit()
    {

        if (LockedInteractors.Count == 0 && HoveredInteractors.Count>0) //Fonctionne ! -> ajoute le premier objet si liste vide != null
        {
            LockedInteractors.Add(HoveredInteractors[0]);    
        }

        if(LockedInteractors.Count!=0 && HoveredInteractors.Count>0 && !LockedInteractors.Contains(HoveredInteractors[0]))
        {
            LockedInteractors.Add(HoveredInteractors[0]);
        }

        if(LockedInteractors.Count == 2)
        {
            CheckCompatibility();
            LockedInteractors.Clear();
        }
    }

    public void CheckCompatibility()
    {
        Debug.Log("GG");
    }
}
