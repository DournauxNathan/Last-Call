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

    private Color baseColor;
    public Color lockColor;
    public bool isLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        baseColor = GetComponent<Outline>().OutlineColor;
        subList.Add(gameObject);

        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }
        outline.enabled = false;

        if (GameObject.Find("ObjetAactiver") != null)
        {
            init = GameObject.Find("ObjetAactiver").GetComponent<ObjetcActivatorImaginaire>();        

            if(init.listeObjetByIndex.ContainsKey(id) )
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
    }

    public void Enable()
    {
        outline.enabled = true;
    }

    public void Disabled()
    {
        if (!isLocked)
        {
            outline.enabled = false;
        }
        
    }

    public void Locked()
    {
        isLocked = true;
        outline.OutlineColor = lockColor;
    }

    public void UnLocked()
    {
        isLocked = false;
        outline.OutlineColor = baseColor;
        Disabled();
    }
}
