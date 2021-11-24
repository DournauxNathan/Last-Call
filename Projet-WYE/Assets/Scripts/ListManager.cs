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
            HoveredInteractors[0].GetComponent<ObjectManager>().Locked();
        }

        if(LockedInteractors.Count!=0 && HoveredInteractors.Count>0 && !LockedInteractors.Contains(HoveredInteractors[0]))
        {
            LockedInteractors.Add(HoveredInteractors[0]);
            HoveredInteractors[0].GetComponent<ObjectManager>().Locked();
        }

        if(LockedInteractors.Count == 2)
        {
            CheckCompatibility(LockedInteractors[0], LockedInteractors[1]);
            for (int i = 0; i < LockedInteractors.Count; i++)
            {
                LockedInteractors[i].GetComponent<ObjectManager>().UnLocked();
            }
            LockedInteractors.Clear();
        }
    }

    public void PressToCancel()
    {
        for (int i = 0; i < LockedInteractors.Count; i++)
        {
            LockedInteractors[i].GetComponent<ObjectManager>().UnLocked();
        }
        LockedInteractors.Clear();
    }


    public void CheckCompatibility(GameObject objet1,GameObject objet2)
    {
        Combinable combinable;

        if (objet1.TryGetComponent<Combinable>(out combinable))
        {
            if (combinable.combineWith == objet2)
            {
                Debug.Log("c'est le meme");
            }
            else
            {
                Debug.Log("c'est pas le meme");
            }
        }
        else if(objet2.TryGetComponent<Combinable>(out combinable))
        {
            if (combinable.combineWith == objet1)
            {
                Debug.Log("c'est le meme");
            }
            else
            {
                Debug.Log("c'est pas le meme");
            }
        }
        else
        {
            Debug.Log("No one has combinable");
        }
    }

    public void DebugController()
    {
        Debug.Log("Click !");
    }
}
