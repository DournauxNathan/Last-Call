using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    [Header("Refs")]
    public Renderer meshRender;
    public CapsuleCollider capsuleCollider;
    public SphereCollider sphereCollider;

    [HideInInspector] public bool isInstiantiated;
    [HideInInspector] public bool hasMove = false;

    private Transform stock;
    public bool isActive = false;

    private void Start()
    {
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Eat();
        }        
    }*/

    public void Activate(Transform stock)
    {
        this.stock = stock;

        this.meshRender.enabled = true;
        this.capsuleCollider.enabled = true;
        this.sphereCollider.enabled = true;

        isActive = true;
        isInstiantiated = true;
    }

    public void Move(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.SetParent(null);

        hasMove = true;
    }

    public void Desactivate()
    {
        isActive = false;
    }

    public void ReputOnStock()
    {
        this.meshRender.enabled = false;
        this.capsuleCollider.enabled = false;
        this.sphereCollider.enabled = false;

        Desactivate();
        transform.SetParent(stock);
        isInstiantiated = false;
    }

    public void Eat()
    {
        ReputOnStock();
        MasterManager.Instance.currentPills++;
    }
}
