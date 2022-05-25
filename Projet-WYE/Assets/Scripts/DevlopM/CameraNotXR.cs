using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNotXR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(MasterManager.Instance != null) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
