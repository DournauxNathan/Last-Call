using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof (SphereCollider))]
public class Combinable : MonoBehaviour
{
    public GameObject combineWith;
    public string resultOrder;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjCombi"))
        {
            this.gameObject.SetActive(false);
            combineWith.SetActive(false);

            OrderController.instance.IncreaseValue(1);
            OrderController.instance.DisplayOrderList(resultOrder);
            OrderController.instance.orders.Add(resultOrder);
        }
    }
}
