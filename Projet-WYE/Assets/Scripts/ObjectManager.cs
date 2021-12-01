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

    private ObjetcActivatorImaginaire init;
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
        if (/*SceneLoader.Instance.GetActiveScene().name == "Imaginary"*/GameObject.FindObjectOfType<ObjetcActivatorImaginaire>().inImaginaire)
        {
            outlineManager.isLocked = true;
            outlineManager.outline.OutlineColor = outlineManager.selectColor;
        }
    }

    public void UnLocked()
    {
        if (/*SceneLoader.Instance.GetActiveScene().name == "Imaginary"*/GameObject.FindObjectOfType<ObjetcActivatorImaginaire>().inImaginaire)
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
            this.gameObject.SetActive(false);
            combinable.combineWith[0].SetActive(false);

            OrderController.instance.IncreaseValue(1);
            OrderController.instance.DisplayOrderList(combinable.resultOrder);
            OrderController.instance.orders.Add(combinable.resultOrder);
        }
    }


}

[System.Serializable]
public class Combinable
{
    public List<GameObject> combineWith = new List<GameObject>();
    public string resultOrder;
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