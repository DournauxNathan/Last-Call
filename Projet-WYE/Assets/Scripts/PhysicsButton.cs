using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhysicsButton : MonoBehaviour
{
    [Header("References")]
    public Rigidbody buttonTopRigid;
    public Transform buttonTop;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    public CanvasGroup icon;
    private Renderer _meshRenderer;
    private Vector3 savedPosition;
    private string savedUnit;

    [Header("Parameters")]
    public float threshHold;
    public float force = 10;
    private float upperLowerDiff;
    private bool prevPressedState;
    public Collider[] CollidersToIgnore;
    [Space(2)]
    public bool isPressed;
    public int nPress = 0;

    [Header("SFX")]
    public AudioClip pressedSound;
    public AudioClip releasedSound;
    private AudioSource _audioSource;

    [Header("Events")]
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    [Header("State Color")]
    public bool isActivate;
    [SerializeField] private Material activateColor;
    [SerializeField] private Material unavailableColor;
    [SerializeField] private Material desactivateColor;

    [Header("Debug")]
    public bool useEvents;
    public bool debugMethod = false;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = buttonTop.GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
        
        UnitManager.Instance.physicsbuttons.Add(this);

        ChangeStateColor(isActivate);

        Collider localCollider = GetComponent<Collider>();
        if (localCollider != null)
        {
            Physics.IgnoreCollision(localCollider, buttonTop.GetComponentInChildren<Collider>());

            foreach (Collider singleCollider in CollidersToIgnore)
            {
                Physics.IgnoreCollision(localCollider, singleCollider);
            }
        }

        if (transform.eulerAngles != Vector3.zero)
        {
            savedPosition = transform.position;
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (buttonTop.localPosition.y >= 0)
            buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        else
            buttonTopRigid.AddForce(buttonTop.transform.up * force * Time.deltaTime);

        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)
            buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);

        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) <= upperLowerDiff * threshHold)
            isPressed = true;
        else
            isPressed = false;
/*
        #region Press Events / Methods
        //Using Events method (multiple reference)
        if (isPressed && prevPressedState != isPressed && useEvents)
        {
            onPressed?.Invoke();

            if (debugMethod)
                Debug.LogWarning("Using events invoke");
        }
        else if (isPressed && prevPressedState != isPressed)
        {
            Pressed();

            if (debugMethod)
                Debug.LogWarning("Using direct method");
        }
        #endregion

        #region Release Events / Methods
        *//*
        //Using method in script (direct reference)
        if (!isPressed && prevPressedState != isPressed)
        {
            onReleased?.Invoke();

            if (debugMethod)
                Debug.LogWarning("Using events invoke");
        }
        else if (!isPressed && prevPressedState != isPressed)
        {
            Released();

            if (debugMethod)
                Debug.LogWarning("Using direct method");
        }*//*
        #endregion*/
    }
    
    public void Pressed()
    {
        prevPressedState = isPressed;
        if (pressedSound != null)
        {
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(pressedSound);
        }
        InvokeEvent(onPressed);

        if (UIManager.Instance.currentForm.isComplete)
        {
            UIManager.Instance.SetFormToComplete(true);
        }
        else
        {
            UIManager.Instance.SetFormToComplete(false);
        }        
    }

    public void InvokeEvent(UnityEvent _event)
    {
        _event?.Invoke();
    }

    public void Released()
    {
        prevPressedState = isPressed;
        if (releasedSound != null)
        {
            _audioSource.pitch = UnityEngine.Random.Range(1.1f, 1.2f);
            _audioSource.PlayOneShot(releasedSound);
        }
        InvokeEvent(onReleased);
    }
    public void ChangeStateColor(bool state)
    {
        if (state)
        {
            _meshRenderer.material = activateColor;
            buttonTop.GetComponent<Collider>().enabled = true;
        }
        else
        {
            _meshRenderer.material = desactivateColor;
            buttonTop.GetComponent<Collider>().enabled = false;
        }
    }
    public void RegisterUnit(int value)
    {
        switch (value)
        {
            case 1:
                if (!UnitManager.Instance.unitsSend.Contains(Unit.EMS))
                {
                    savedUnit = "Emergency Medical Services";

                    UnitManager.Instance.unitsSend.Add(Unit.EMS);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.EMS);

                }
                else
                {
                    Debug.LogWarning(Unit.EMS + "have alreadu been register");
                }
                break;

            case 2:
                if (!UnitManager.Instance.unitsSend.Contains(Unit.Police))
                {
                    savedUnit = "Police";

                    UnitManager.Instance.unitsSend.Add(Unit.Police);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.Police);
                }
                else
                {
                    Debug.LogWarning(Unit.Police + "have alreadu been register");
                }
                break;

            case 3:
                if (!UnitManager.Instance.unitsSend.Contains(Unit.FireDepartment))
                {
                    savedUnit = "Fire Department";

                    UnitManager.Instance.unitsSend.Add(Unit.FireDepartment);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.FireDepartment);
                }
                else
                {
                    Debug.LogWarning(Unit.FireDepartment + "have alreadu been register");
                }
                break;

            case 4:
                if (!UnitManager.Instance.unitsSend.Contains(Unit.SWAT))
                {
                    savedUnit = "S.W.A.T";

                    UnitManager.Instance.unitsSend.Add(Unit.SWAT);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.SWAT);
                }
                else
                {
                    Debug.LogWarning(Unit.SWAT + "have alreadu been register");
                }
                break;
        }
    }
    public void UnStockButton()
    {
        buttonTop.position = buttonUpperLimit.position;
    }
    public void IncreasePressDetection(int value)
    {
        nPress += value;
        NumberOfPress(nPress);
    }
    public void NumberOfPress(int value)
    {
        if (value == 1)
        {
            UnitManager.Instance.UpdateUI();

            nPress = 1;
        }
        else if (value > 2)
        {
            _meshRenderer.material = unavailableColor;

            UnitManager.Instance.NextSequence();
            UnitManager.Instance.UpdateUI();

            UIManager.Instance.UpdateForm(FormData.unit, savedUnit);

            nPress = 2;
        }
    }

    public void ChangeSceneButtun()
    {
        SceneLoader.Instance.LoadNewScene("Office");
    }
}