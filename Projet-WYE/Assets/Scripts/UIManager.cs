using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UIManager : Singleton<UIManager>
{
    [Header("Screens Canvas")]
    public CanvasGroup[] _canvasGroup;
        
    public ParticleSystem smoke;

    private bool fadeOut = false;
    private bool fadeIn = false;

    public void Fade(Fadetype type)
    {
        switch (type)
        {
            case Fadetype.In:
                foreach (CanvasGroup canvas in _canvasGroup)
                {
                    if (canvas.alpha < 1)
                    {
                        canvas.alpha += Time.deltaTime;

                        if (canvas.alpha >= 1)
                        {
                            smoke.Play();
                            fadeIn = false;
                        }
                    }
                }
                break;

            case Fadetype.Out:
                foreach (CanvasGroup canvas in _canvasGroup)
                {
                    if (canvas.alpha < 1)
                    {
                        canvas.alpha += Time.deltaTime;

                        if (canvas.alpha >= 1)
                        {
                            smoke.Play();
                            fadeIn = false;
                        }
                    }
                }
                break;
        }
    }
}

public enum Fadetype
{
    In,
    Out
}
