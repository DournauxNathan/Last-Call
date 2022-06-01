using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.Events;

public class ShakeWord : MonoBehaviour
{
    [Header("Refs")]
    public CanvasGroup alpha;
    public Image image;

    [Header("Params")]
    [Range(0f, 2f)] public float delayBeforAnim = 0f;
    [Range(0f, 2f)] public float animationSpeed = 1f;
    public Color outlineColor;
    [Range(0f, 1f)] public float outlineWidth = 0.135f;
    public Color validateColor;

    public Color defaultColor;
    public Color hoverColor;

    public UnityEvent submitWord;

    private bool isStarted;

    [Header("Debug")]
    [SerializeField] Animator m_animator;
    public bool isDecaying = false;
   

    private float _time;
    private TMP_Text _text;


    private void Start()
    {
        if (image != null)
        {
            defaultColor = image.color;
        }

        alpha = GetComponent<CanvasGroup>();
        _time = delayBeforAnim;
        _text = GetComponentInChildren<TMP_Text>();
        //m_animator = GetComponent<Animator>();
        //IsSelected(); Validate();

        submitWord.AddListener(SendToSaveFile);
    }

    private void Update()
    {
        if (m_animator != null && m_animator.speed != animationSpeed)
        {
            m_animator.speed = animationSpeed;
            CheckAnimStart();
        }

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
            //m_animator.SetBool("Bool", true);
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
                submitWord?.Invoke();

                GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void OnHoverEnter()
    {
        image.color = hoverColor;
        //m_animator.SetBool("Bool", false);
    }

    public void OnHoverExit()
    {
        image.color = defaultColor;
        //m_animator.SetBool("Bool", true);
    }

    public void IsSelected()
    {
        Debug.Log("is selected");
        //m_animator.SetBool("Bool", false);

        image.color = outlineColor;
        if (image != null)
        {
            image.color = outlineColor;
        }

    }

    public void DeSelected()
    {
        image.color = defaultColor;/*
        image.outlineColor = _defaultColorOutline;
        image.outlineWidth = 0f;*/
        //m_animator.SetBool("Bool", true);
    }

    public void Validate()
    {
        m_animator.SetBool("Bool", false);
        image.color = validateColor;
        isDecaying = true;
        //submitWord?.Invoke();
        StartFadeOut(alpha);
    }


    private void SendToSaveFile()
    {
        SaveQuestion.Instance.AddQuestion(_text.text);
    }

}
