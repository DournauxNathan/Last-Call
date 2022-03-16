using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : Singleton<HandController>
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacterisitcs;
    public List<GameObject> controllerPrefabs;
    public GameObject handmodelPrefab;

    [HideInInspector] public InputDevice targetDevice;
    private GameObject spawnController;
    private GameObject spawnHandModel;

    private Animator handAnimator;

    private Vector3 acceleration;
    public int indexTab = 0;
    bool lButton, rButton;


    // Start is called before the first frame update
    void Start()
    {
        TryInitialise();
    }

    void TryInitialise()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacterisitcs, devices);

       /* foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }*/

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

        if (targetDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 _acceleration))
        {
            acceleration = _acceleration;
            //Debug.Log(_acceleration);
        }

        #region Secondary Button
        if (targetDevice.name == "Oculus Touch Controller - Left" && targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out lButton)
            || targetDevice.name == "Oculus Touch Controller - Right" && targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out rButton))
        {
            if (lButton || rButton)
            {
                Projection.Instance.isTransition = true;
            }
            else
            {
                Projection.Instance.ResetTransition();
            }            
        }
        #endregion


        #region Joystick Button
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool _rClick))
        {
            if (_rClick)
            {
                _rClick = false;
            indexTab++;

                if (indexTab >= 3)
                {
                    indexTab = 0;
                }

                UiTabSelection.Instance.UpdateIndex(indexTab);
                UiTabSelection.Instance.SwitchTab(indexTab);
            }
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool _lClick))
        {
            if (_lClick)
            {
                _lClick = false;
                indexTab++;

                if (indexTab >= 3)
                {
                    indexTab = 0;

                    Debug.Log(indexTab);
                }
            }

            UiTabSelection.Instance.UpdateIndex(indexTab);
            UiTabSelection.Instance.SwitchTab(indexTab);
        }
        #endregion
    }

    public Vector3 GetDeviceAccelation()
    {
        //Debug.LogWarning("Hand are need to test this feature !");
        return acceleration;
    }

    // Update is called once per frame
    void FixedUpdate()
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
