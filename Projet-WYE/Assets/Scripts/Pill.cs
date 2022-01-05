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

    private Transform stock;
    private bool isActive = false;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Eat();
        }        
    }
    public void Activate(Transform parent, Transform stock)
    {
        transform.SetParent(parent);
        transform.position = parent.position;

        this.stock = stock;

        this.meshRender.enabled = true;
        this.capsuleCollider.enabled = true;
        this.sphereCollider.enabled = true;

        isActive = true;
        isInstiantiated = true;

        transform.SetParent(null);
    }

    public void Desactivate()
    {
        isActive = false;
    }
    private void ReputOnStock()
    {
        this.meshRender.enabled = false;
        this.capsuleCollider.enabled = false;
        this.sphereCollider.enabled = false;

        Desactivate();
        transform.SetParent(stock);
        isInstiantiated = false;
    }
    private void Eat()
    {
        ReputOnStock();
        MasterManager.Instance.currentPills++;
    }
}
