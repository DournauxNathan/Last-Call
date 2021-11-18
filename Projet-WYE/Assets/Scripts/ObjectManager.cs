using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(Outline))]


public class ObjectManager : MonoBehaviour
{
    public Outline outline;
    public int id;
    private ObjetcActivatorImaginaire init;
    public List<GameObject> subList;
    //private GameObject[] subList = new GameObject[1];


    // Start is called before the first frame update
    void Start()
    {
        subList.Add(gameObject);
        outline.GetComponent<Outline>().enabled = false;
        init = GameObject.Find("ObjetAactiver").GetComponent<ObjetcActivatorImaginaire>();

        if(init.listeObjetByIndex.ContainsKey(id))
        {
            List<GameObject> tempObject;

            tempObject = init.listeObjetByIndex[id];
            subList.AddRange(tempObject);
            init.listeObjetByIndex.Remove(id);
            init.listeObjetByIndex.Add(id, subList);
            return;
        }
        else
        {
            init.listeObjetByIndex.Add(id, subList);
        }
    }

    public void Enable()
    {
        outline.enabled = true;
    }

    public void Disabled()
    {
        outline.enabled = false;
    }
}
