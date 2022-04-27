using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress, onRelease;

    public AudioClip pushSFX;

    private GameObject presser;
    private AudioSource _audioSource;

    private bool isPressed;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.CompareTag("Hands"))
        {
            Debug.Log("Pres");
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;

            onPress?.Invoke();
            _audioSource.PlayOneShot(pushSFX);
            isPressed = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == presser)
        {
            button.transform.localPosition = new Vector3(0, 015f, 0);
            onRelease?.Invoke();
            isPressed = false;
        }   
    }

    public void DebugPress()
    {
        Debug.Log("Press");
    }

    public void DebugRelease()
    {
        Debug.Log("Release");
    }
}
