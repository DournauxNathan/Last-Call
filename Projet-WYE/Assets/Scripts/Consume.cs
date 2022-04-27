using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pill")
        {
            other.GetComponent<Pill>().ReputOnStock();
            //MasterManager.Instance.currentPills++;
        }
        else if (other.tag == "Bonbon")
        {
            Destroy(other.gameObject);
        }
    }
}
