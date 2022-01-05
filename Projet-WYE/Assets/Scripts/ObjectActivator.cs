using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : Singleton<ObjectActivator>
{
    [Header("Objects to activate")]
    public List<GameObject> objectsList;

    public Dictionary<int, List<GameObject>> objectByIdList = new Dictionary<int, List<GameObject>>();

    public List<int> indexesList;
    public bool inImaginaire = false;

    public List<GameObject> desactivatedObject = new List<GameObject>();

    public ObjectManager[] uselessObjects;

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

        for (int i = 0; i < objectsList.Count; i++)
        {
            if (objectsList[i].GetComponent<ObjectManager>() != null)
            {
                //objectsList[i].GetComponent<ObjectManager>().outlineManager.Enable();
            }
            else
            {
                Debug.LogError("Erreur l'objet " + i + " n'a pas le script Highlight !");
            }            
        }

        inImaginaire = true;
        MasterManager.Instance.isInImaginary = inImaginaire;

        if (inImaginaire)
        {
            ActivateUselessObject();
        }
    }  

    public void ActivateUselessObject()
    {
        uselessObjects = FindObjectsOfType<ObjectManager>();

        foreach (var obj in uselessObjects)
        {
            
        }

    }

    public void SetActivetObject(GameObject[] _list)
    {
        objectsList.AddRange(_list);

        foreach (var item in _list)
        {
            desactivatedObject.Add(item);

            if (!indexesList.Contains(item.GetComponent<ObjectManager>().data.iD))
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