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
    

    private Color _defaultColorOutline;
    private Color _defaultColor;

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
        _defaultColorOutline = _text.outlineColor;
        _defaultColor = _text.color;
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
                submitWord.Invoke();

                GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void OnHoverEnter()
    {
        m_animator.SetBool("Bool", false);
        image.color = hoverColor;
    }

    public void OnHoverExit()
    {
        m_animator.SetBool("Bool", true);
        image.color = defaultColor;
    }

    public void IsSelected()
    {
        m_animator.SetBool("Bool", false);

        if (image != null)
        {
            image.color = outlineColor;
        }

        _text.outlineColor = outlineColor;
        _text.outlineWidth = outlineWidth;
    }

    public void DeSelected()
    {
        image.color = defaultColor;
        _text.outlineColor = _defaultColorOutline;
        _text.outlineWidth = 0f;
        m_animator.SetBool("Bool", true);
    }

    public void Validate()
    {
        m_animator.SetBool("Bool", false);
        _text.color = validateColor;
        isDecaying = true;
        StartFadeOut(alpha);
    }

    public void Debuggg()
    {
        Debug.Log("COUCOU");
    }

    private void SendToSaveFile()
    {
        SaveQuestion.Instance.AddQuestion(_text.text);
    }

}
