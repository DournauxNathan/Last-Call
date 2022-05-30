using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNotXR : MonoBehaviour
{
    void Start()
    {
        if(MasterManager.Instance != null) gameObject.SetActive(false);
    }
}
