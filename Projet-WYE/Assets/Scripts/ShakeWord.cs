using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.Events;

public class ShakeWord : MonoBehaviour
{
    [Header("Refs")]
    public CanvasGroup alpha;

    [Header("Param")]
    [Range(0f, 2f)] public float delayBeforAnim = 0f;
    [Range(0f, 2f)] public float animationSpeed = 1f;
    public Color32 outlineColor;
    [Range(0f, 1f)] public float outlineWidth = 0.135f;
    public Color32 validateColor;
    [Range(1, 5)] public byte decaySpeed = 1;
    [Header("Events")]
    public UnityEvent OnValidateWord;


    private bool isStarted;
    [Header("Debug")]
    [SerializeField] Animator m_animator;
    [SerializeField] private byte decayValue = 255;
    private bool isDecaying = false;
    private float _time;
    private TMP_Text _text;
    private Color32 _defaultColorOutline;
    private Color32 _defaultColor;

    

    private void Start()
    {
        alpha = GetComponent<CanvasGroup>();
        _time = delayBeforAnim;
        _text = GetComponentInChildren<TMP_Text>();
        _defaultColorOutline = _text.outlineColor;
        _defaultColor = _text.color;
        //m_animator = GetComponent<Animator>();
        //IsSelected(); Validate();
    }

    private void Update()
    {
        if (m_animator.speed != animationSpeed)
        {
            m_animator.speed = animationSpeed;
        }

        CheckAnimStart();
        TextDecay();
    }

    private void CheckAnimStart()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }

        else if (!isStarted && _time <= 0)
        {
            isStarted = true;
            m_animator.SetBool("Bool", true);
        }
    }

    private void TextDecay()
    {
        if (isDecaying)
        {
            StartFadeOut(alpha);
        }        
    }

    public void StartFadeOut(CanvasGroup uiGroupToFade)
    {
        if (uiGroupToFade.alpha >= 0)
        {
            uiGroupToFade.alpha -= Time.deltaTime;

            if (uiGroupToFade.alpha == 0)
            {
                isDecaying = false;
                OnValidateWord.Invoke();
                GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }


    public void IsSelected()
    {
        m_animator.SetBool("Bool", false);
        _text.outlineColor = outlineColor;
        _text.outlineWidth = outlineWidth;
    }

    public void DeSelected()
    {
        _text.outlineColor = _defaultColorOutline;
        _text.outlineWidth = 0f;
        m_animator.SetBool("Bool", true);
    }

    public void Validate()
    {
        m_animator.SetBool("Bool", false);
        _text.color = validateColor;
        isDecaying = true;
    }

    public void Debuggg()
    {
        Debug.Log("COUCOU");
    }


}
