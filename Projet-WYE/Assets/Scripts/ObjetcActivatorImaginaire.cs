using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcActivatorImaginaire : MonoBehaviour
{

    [Header("Paramètre des objets a activé")]
    public List<GameObject> listeobject;

    public Dictionary<int, List<GameObject>> listeObjetByIndex = new Dictionary<int, List<GameObject>>();

    public List<int> listeIndex;
    [SerializeField]
    public bool inImaginaire = false;


    // Start is called before the first frame update
    void Start()
    {
        /*if(nbobjetactif > listeobject.Count)
        {
            nbobjetactif = listeobject.Count;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObjet()
    {
        for (int i = 0; i < listeIndex.Count; i++)
        {
            foreach (var index in listeObjetByIndex)
            {
                if(listeIndex[i] == index.Key)
                {
                    listeobject.AddRange(index.Value);
                }
            }   
        }


        for (int i = 0; i < listeobject.Count; i++)
        {
            if (listeobject[i].GetComponent<ObjectManager>() != null)
            {
                //listeobject[i].GetComponent<ObjectManager>().outlineManager.Enable();
            }
            else
            {
                Debug.LogError("Erreur l'objet " + i + " n'a pas le script Highlight !");
            }
            
        }
        inImaginaire = true;

    }
    

}
