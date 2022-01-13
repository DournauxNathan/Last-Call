using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projection : Singleton<Projection>
{
    [Header("Refs")]
    public Transform player;
    [Space(5)]public List<Material> transitionShaders;

    [Header("Parameters")]
    public bool startTransition;
    [Tooltip("During of te transition in seconds")] public float time;
    [Tooltip("0: Go to the imaginary | 1: Go Back to real life")]public int transitionValue;
    [Range(0,3)] public float range = 3f;
    [SerializeField, Tooltip("Hide UI at this value")] private float beginFadeOutAt;
    [SerializeField, Tooltip("Show UI at this value")] private float beginFadeInAt;

    private Vector3 playerPos;
    private bool fadeOut = false;
    private bool fadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.position;

        foreach (var mat in transitionShaders)
        {
            mat.SetVector("_PlayerPos", playerPos);
            mat.SetFloat("_Distance", 3f * 10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransition)
        {
            DoTransition(transitionValue);
        }

        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Distance", range * 10f);
        }
        
        if (range <= beginFadeOutAt)
        {
            HideUI();
        }

        if (range == beginFadeInAt)
        {
            ShowUI();
        }


        if (fadeIn) //Show UI
        {
            if (UIManager.Instance.leftScreen != null)
            {
                StartFadeIn(UIManager.Instance.leftScreen);
                StartFadeIn(UIManager.Instance.rightScreen);
            }
        }

        if (fadeOut) //Hide UI
        {
            if (UIManager.Instance.leftScreen != null)
            {
                StartFadeOut(UIManager.Instance.leftScreen);
                StartFadeOut(UIManager.Instance.rightScreen);
            }
        }
    }

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    public void StartFadeIn(CanvasGroup uiGroupToFade)
    {
        if (uiGroupToFade.alpha < 1)
        {
            uiGroupToFade.alpha += Time.deltaTime;

            if (uiGroupToFade.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }

    public void StartFadeOut(CanvasGroup uiGroupToFade)
    {
        if (uiGroupToFade.alpha >= 0)
        {
            uiGroupToFade.alpha -=  Time.deltaTime;

            if (uiGroupToFade.alpha == 0)
            {
                fadeOut = false;
            }
        }
    }

    public void DoTransition(int state)
    {
        if (state == 0)
        {
            range -= time * Time.deltaTime;

            if (range <= 0)
            {
                startTransition = false;
                transitionValue = 1;
            
                range = 0;
                MasterManager.Instance.ActivateImaginary("Call1");
            }
        }
        else if (state == 1)
        {
            range += time *Time.deltaTime;

            if (range >= 2.5)
            {
                startTransition = false;
                transitionValue = 0;
                range = 5;
                
            }
        }
    }
}
