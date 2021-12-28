using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Outline), typeof(SphereCollider))]
public class ObjectManager : MonoBehaviour
{
    public int id;
    public List<GameObject> subList; //???? What is it ? Can't Remember ?

    public Combinable combinable;
    public OutlineManager outlineManager;

    private ObjectActivator init;
    private void Awake()
    {
        SetTriggerCollide(true);
        SetOutline();
    }

    // Start is called before the first frame update
    void Start()
    {
        subList.Add(gameObject);   

        if (GameObject.Find("ObjetAactiver") != null)
        {
            init = GameObject.Find("ObjetAactiver").GetComponent<ObjectActivator>();        

            if(init.objectByIdList.ContainsKey(id) )
            {
                List<GameObject> tempObject;

                tempObject = init.objectByIdList[id];
                subList.AddRange(tempObject);
                init.objectByIdList.Remove(id);
                init.objectByIdList.Add(id, subList);
                return;
            }
            else
            {
                init.objectByIdList.Add(id, subList);
            }
        }
    }

    private void SetOutline()
    {
        if (outlineManager.outline == null)
        {
            outlineManager.outline = GetComponent<Outline>();
        }

        outlineManager.outline.enabled = false;

        outlineManager.baseColor = GetComponent<Outline>().OutlineColor;
        outlineManager.selectColor = outlineManager.selectOutline.color;
    }

    private void SetTriggerCollide(bool newState)
    {
        if (GetComponents<SphereCollider>().Length == 2)
        {
            GetComponents<SphereCollider>()[1].isTrigger = newState;
        }
        else
        {
            GetComponent<SphereCollider>().isTrigger = newState;
        }
    }

    public void Enable()
    {
        outlineManager.outline.enabled = true;
    }

    public void Disabled()
    {
        if (!outlineManager.isLocked)
        {
            outlineManager.outline.enabled = false;
        }
    }

    public void Locked()
    {
        if (GameObject.FindObjectOfType<ObjectActivator>().inImaginaire)
        {
            outlineManager.isLocked = true;
            outlineManager.outline.OutlineColor = outlineManager.selectColor;
        }
    }

    public void UnLocked()
    {
        if (GameObject.FindObjectOfType<ObjectActivator>().inImaginaire)
        {
            outlineManager.isLocked = false;
            outlineManager.outline.OutlineColor = outlineManager.baseColor;
            Disabled();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjCombi"))
        {
            ListManager.Instance.CheckCompatibility(this.gameObject, other.gameObject);
        }

        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Disable hand collider");
            other.GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Enable hand collider");
            other.GetComponent<MeshCollider>().enabled = true;
        }
    }
}

[System.Serializable]
public class Combinable
{
    public List<GameObject> combineWith = new List<GameObject>();
    public OrderFormat resultOrder;
    public bool isStatic;
}

[System.Serializable]
public class OutlineManager
{
    public Material selectOutline;
    public bool isLocked = false;

    [HideInInspector] public Outline outline;
    [HideInInspector] public Color baseColor;
    [HideInInspector] public Color selectColor;

 
}