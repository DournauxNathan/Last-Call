using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcActivatorImaginaire : MonoBehaviour
{

    [Header("Paramètre des objets a activé")]
    [Tooltip("Priorité de haut en bas")]
    public GameObject[] listeobject;
    [Tooltip("Nombre d'objet a activé")]
    public int nbobjetactif = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(nbobjetactif > listeobject.Length)
        {
            nbobjetactif = listeobject.Length;
        }
        ActivateObjet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateObjet()
    {
        for (int i = 0; i < nbobjetactif; i++)
        {
            if (listeobject[i].GetComponent<Highlight>() != null)
            {
                listeobject[i].GetComponent<Highlight>().Enable();
            }
            else
            {
                Debug.LogError("Erreur l'objet " + i + " n'a pas le script Highlight !");
            }
            
        }

    }

}
