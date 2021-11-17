using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacterisitcs;
    public List<GameObject> controllerPrefabs;
    public GameObject handmodelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnController;
    private GameObject spawnHandModel;

    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialise();
    }

    void TryInitialise()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacterisitcs, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            if (prefab)
            {
                spawnController = Instantiate(prefab, transform);

            }
            else
            {
                Debug.LogError("Did no find corresponding controller model");
                spawnController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnHandModel = Instantiate(handmodelPrefab, transform);
            handAnimator = spawnHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {        
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) /*&& triggerValue > 0.1f*/)
        {
            handAnimator.SetFloat("Trigger", triggerValue);
            //Debug.Log("Trigger pressed: " + triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0f);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) /*&& gripValue > 0.1f*/)
        {
            handAnimator.SetFloat("Grip", gripValue);
            //Debug.Log("Primary touchpad: " + primary2DAxisValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialise();
        }
        else
        {
            if (showController)
            {
                spawnHandModel.SetActive(false);
                spawnController.SetActive(true);
            }
            else
            {
                spawnHandModel.SetActive(true);
                spawnController.SetActive(false);

                UpdateHandAnimation();
            }
        }        
    }
}
