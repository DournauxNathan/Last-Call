using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ListManager : MonoBehaviour
{
    public List<GameObject> hoveredInteractors;
    public List<GameObject> lockedInteractors;

    public void ClearList()
    {
        hoveredInteractors.Clear();
    }

    public void OnPressed()
    {
        if(hoveredInteractors.Count != 0)
        {
            if (!lockedInteractors.Contains(hoveredInteractors[0]))
            {
                Select();
            }
            else if (lockedInteractors.Contains(hoveredInteractors[0]))
            {
                UnSelect();
            }
        }
    }

    public void Select()
    {
        if (lockedInteractors.Count == 0 && hoveredInteractors.Count > 0) //Fonctionne ! -> ajoute le premier objet si liste vide != null
        {
            lockedInteractors.Add(hoveredInteractors[0]);
            hoveredInteractors[0].GetComponent<ObjectManager>().Locked();
        }

        if (lockedInteractors.Count != 0 && hoveredInteractors.Count > 0 && !lockedInteractors.Contains(hoveredInteractors[0]))
        {
            lockedInteractors.Add(hoveredInteractors[0]);
            hoveredInteractors[0].GetComponent<ObjectManager>().Locked();
        }

        if (lockedInteractors.Count == 2)
        {
            CheckCompatibility(lockedInteractors[0], lockedInteractors[1]);

            for (int i = 0; i < lockedInteractors.Count; i++)
            {
                lockedInteractors[i].GetComponent<ObjectManager>().UnLocked();
            }
            lockedInteractors.Clear();
        }
    }

    public void UnSelect()
    {
        for (int i = 0; i < lockedInteractors.Count; i++)
        {
            lockedInteractors[i].GetComponent<ObjectManager>().UnLocked();
        }
        lockedInteractors.Clear();
    }


    public void CheckCompatibility(GameObject objet1,GameObject objet2)
    {
       // ObjectManager _objectManager;
        ObjectManager _objectManager1;
        ObjectManager _objectManager2;

        if (objet1.TryGetComponent<ObjectManager>(out _objectManager1) && objet2.TryGetComponent<ObjectManager>(out _objectManager2))
        {
            if (_objectManager1.combinable.combineWith == objet2)
            {
                objet1.SetActive(false);
                objet2.SetActive(false);


                OrderController.instance.IncreaseValue(1);
                OrderController.instance.DisplayOrderList(_objectManager1.combinable.resultOrder);
                OrderController.instance.orders.Add(_objectManager1.combinable.resultOrder);
            }
            else if (_objectManager2.combinable.combineWith == objet1)
            {
                objet1.SetActive(false);
                objet2.SetActive(false);


                OrderController.instance.IncreaseValue(1);
                OrderController.instance.DisplayOrderList(_objectManager2.combinable.resultOrder);
                OrderController.instance.orders.Add(_objectManager2.combinable.resultOrder);
            }
            else if (_objectManager1.combinable.combineWith == null || _objectManager2.combinable.combineWith == null)
            {
                Debug.Log("Can't combine them");
            }
        }
        else
        {
            Debug.LogWarning("No one has the Combinable component");
        }

        /*
        if (objet1.TryGetComponent<ObjectManager>(out _objectManager))
        {
            if (_objectManager.combinable.combineWith == objet2)
            {
                objet1.SetActive(false);
                objet2.SetActive(false);
            }

            else
            {
                Debug.LogWarning("Can't combine because it's the same object");
            }
        }
        else if(objet2.TryGetComponent<ObjectManager>(out _objectManager))
        {
            if (_objectManager.combinable.combineWith == objet1)
            {
                objet1.SetActive(false);
                objet2.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Can't combine because it's the same object");
            }
        }
        else
        {
            Debug.LogWarning("No one has the Combinable component");
        }*/
    }
}
