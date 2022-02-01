using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumePills : MonoBehaviour
{
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pill")
        {
            other.GetComponent<Pill>().ReputOnStock();
            MasterManager.Instance.currentPills++;
        }
    }
}
