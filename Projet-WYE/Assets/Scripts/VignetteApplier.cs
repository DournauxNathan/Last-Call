using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class VignetteApplier : MonoBehaviour
{
    public float intensity = .75f;
    public float duration = .5f;
    public Volume volume;

    private Vignette vignette;

    public bool blink;
    public bool fadeout;

    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.TryGet(out Vignette vignette))
        {
            this.vignette = vignette;
        }
    }

    private void Update()
    {
        if (blink)
        {
            FadeIn();
            blink = !blink;
        }

        if (fadeout)
        {
            FadeOut();
            fadeout = !fadeout;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, intensity));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(intensity, 0));
    }

    private IEnumerator Fade(float startValue, float endValue)
    {
        float elapsedTime = .0f;

        while (elapsedTime <= duration)
        {
            //Figure out blend value
            elapsedTime += Time.deltaTime;
            float blend = elapsedTime / duration;

            //Apply intenisty
            float intensity = Mathf.Lerp(startValue, endValue, blend);
            ApplyValue(intensity);
        }
        yield return new WaitForSeconds(.2f);
        FadeOut();
        yield return null;

    }

    private void ApplyValue(float value)
    {
        //Override original intensity
        vignette.intensity.Override(value);
    }

}
