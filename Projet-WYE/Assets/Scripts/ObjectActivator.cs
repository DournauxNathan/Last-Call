using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [Header("Paramètre des objets a activé")]
    public List<GameObject> objectsList;

    public Dictionary<int, List<GameObject>> objectByIdList = new Dictionary<int, List<GameObject>>();

    public List<int> indexesList;
    [HideInInspector] public bool inImaginaire = false;

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
    }  
}