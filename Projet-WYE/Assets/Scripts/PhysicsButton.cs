using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [Header("References")]
    public Rigidbody buttonTopRigid;
    public Transform buttonTop;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    private Renderer _meshRenderer;
    private Vector3 savedPosition;

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
    [SerializeField] private Material desactivateColor;

    [Header("Debug")]
    public bool useEvents;
    public bool debugMethod = false;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = buttonTop.GetComponent<Renderer>();
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


        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
            isPressed = true;
        else
            isPressed = false;

        #region Press Events / Methods
        //Using Events method (multiple reference)
        if (isPressed && prevPressedState != isPressed && useEvents)
        {
            NumberOfPress(IncreasePressDetection(1));
            
            onPressed?.Invoke();

            if (debugMethod)
                Debug.LogWarning("Using events invoke");
        }
        else if (isPressed && prevPressedState != isPressed)
        {
            NumberOfPress(IncreasePressDetection(1));

            Pressed();

            if (debugMethod)
                Debug.LogWarning("Using direct method");
        }
        #endregion

        #region Release Events / Methods
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
        }
        #endregion
    }

    public void Pressed()
    {
        prevPressedState = isPressed;
        _audioSource.pitch = 1;
        _audioSource.PlayOneShot(pressedSound);
    }

    public void Released()
    {
        prevPressedState = isPressed;
        _audioSource.pitch = UnityEngine.Random.Range(1.1f, 1.2f);
        _audioSource.PlayOneShot(releasedSound);
    }

    public void ChangeStateColor(bool state)
    {
        if (state)
        {
            _meshRenderer.material = activateColor;
        }
        else
        {
            _meshRenderer.material = desactivateColor;
        }
    }

    public void RegisterUnit(int value)
    {
        switch (value)
        {
            case 1:
                if (!UnitDispatcher.Instance.unitsSend.Contains(Unit.EM))
                {
                    UnitDispatcher.Instance.unitsSend.Add(Unit.EM);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.EM);

                }
                else
                {
                    Debug.LogWarning(Unit.EM + "have alreadu been register");
                }
                break;

            case 2:
                if (!UnitDispatcher.Instance.unitsSend.Contains(Unit.Police))
                {
                    UnitDispatcher.Instance.unitsSend.Add(Unit.Police);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.Police);
                }
                else
                {
                    Debug.LogWarning(Unit.Police + "have alreadu been register");
                }
                break;

            case 3:
                if (!UnitDispatcher.Instance.unitsSend.Contains(Unit.FireDepartment))
                {
                    UnitDispatcher.Instance.unitsSend.Add(Unit.FireDepartment);
                    PlaytestData.Instance.betaTesteurs.data.unitSended.Add(Unit.FireDepartment);
                }
                else
                {
                    Debug.LogWarning(Unit.FireDepartment + "have alreadu been register");
                }
                break;

            case 4:
                if (!UnitDispatcher.Instance.unitsSend.Contains(Unit.SWAT))
                {
                    UnitDispatcher.Instance.unitsSend.Add(Unit.SWAT);
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

    public int IncreasePressDetection(int value)
    {
        return nPress += value;
    }

    public void NumberOfPress(int value)
    {
        if (value == 1)
        {
            UnitDispatcher.Instance.UpdateUI();
        }
        else if (value == 2)
        {
            UnitDispatcher.Instance.NextSequence();
            UnitDispatcher.Instance.UpdateUI();
            nPress = 2;
        }
    }

    public void ChangeSceneButtun()
    {
        SceneLoader.Instance.LoadNewScene("Office");
    }
}