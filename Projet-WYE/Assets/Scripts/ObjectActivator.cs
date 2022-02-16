using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : Singleton<ObjectActivator>
{
    [Header("Objects <Usefull> to activate")]
    public List<GameObject> objectsList;
    public List<int> indexesList;
    public List<GameObject> desactivatedObject = new List<GameObject>();

    public Dictionary<int, List<GameObject>> objectByIdList = new Dictionary<int, List<GameObject>>();

    [Header("Objects <Useless> to activate")]
    public CombinableObject[] uselessObjects;

    public void ActivateObjet()
    {
        for (int i = 0; i < indexesList.Count; i++)
        {
            foreach (var index in objectByIdList)
            {
                if(indexesList[i] == index.Key)
                {
                    objectsList.AddRange(index.Value);
                }
            }   
        }

        /*for (int i = 0; i < objectsList.Count; i++)
        {
            if (objectsList[i].GetComponent<CombinableObject>() != null)
            {
                //objectsList[i].GetComponent<CombinableObject>().outlineManager.Enable();
            }
            else
            {
                Debug.LogError("Erreur l'objet " + i + " n'a pas le script Highlight !");
            }            
        }*/
    }  

    public void ToggleUselessObject(bool enable, int _value)
    {
        uselessObjects = FindObjectsOfType<CombinableObject>();
        
        if (enable)
        {
            for (int i = 0; i < _value; i++)
            {
                //Check and Activate for Grabable object only
                if (uselessObjects[i].GetComponent<XRGrabInteractableWithAutoSetup>())
                {
                    uselessObjects[i].GetComponent<XRGrabInteractableWithAutoSetup>().enabled = true;
                    uselessObjects[i].GetComponent<CombinableObject>().enabled = true;
                }
                else
                {
                    Debug.LogWarning("No Component<XRGrabInteractableWithAutoSetup>() on " + uselessObjects[i].name);
                }

                //Check and Activate for Grabable object only
                if (uselessObjects[i].GetComponent<XRSimpleInteractableWithAutoSetup>())
                {
                    uselessObjects[i].GetComponent<XRSimpleInteractableWithAutoSetup>().enabled = true;
                    uselessObjects[i].GetComponent<CombinableObject>().enabled = true;
                }
                else
                {
                    Debug.LogWarning("No Component<XRSimpleInteractableWithAutoSetup>() on " + uselessObjects[i].name);
                }
            }
        }
        else
        {
            for (int i = 0; i < _value; i++)
            {
                //Check and Activate for Grabable object only
                if (uselessObjects[i].GetComponent<XRGrabInteractableWithAutoSetup>())
                {
                    uselessObjects[i].GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
                    uselessObjects[i].GetComponent<CombinableObject>().enabled = false;
                }
                else
                {
                    Debug.LogWarning("No Component<XRGrabInteractableWithAutoSetup>() on " + uselessObjects[i].name);
                }

                //Check and Activate for Grabable object only
                if ( uselessObjects[i].GetComponent<XRSimpleInteractableWithAutoSetup>())
                {
                    uselessObjects[i].GetComponent<XRSimpleInteractableWithAutoSetup>().enabled = false;
                    uselessObjects[i].GetComponent<CombinableObject>().enabled = false;
                }
                else
                {
                    Debug.LogWarning("No Component<XRSimpleInteractableWithAutoSetup>() on " + uselessObjects[i].name);
                }
            }
        }        
    }

    public void SetActivetObject(GameObject[] _list)
    {
        objectsList.AddRange(_list);

        foreach (var item in _list)
        {
            desactivatedObject.Add(item);

            if (!indexesList.Contains(item.GetComponent<CombinableObject>().iD))
            {
                item.SetActive(false);
            }
        }

        /*for (int i = 0; i < objectsList.Count; i++)
        {
            objectsList[i].SetActive(true);
        }*/
    }
}