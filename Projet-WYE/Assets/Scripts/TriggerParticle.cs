using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particlesSystem;
    [SerializeField] private bool useCountDetection;
    [SerializeField] private int maxCount;
    private int count;

    public void IncreaseCount(int i)
    {
        if (useCountDetection)
        {
            count += i;

            if (count >= maxCount)
            {
                TimeSettings.Instance.Initialize();
            }
        }
        return;
    }
    public void PlayAtIndex(int i)
    {
        particlesSystem[i].Play();
    }
    public void StopAtIndex(int i)
    {

        particlesSystem[i].Play();
    }
    public void PlayAll()
    {
        foreach (ParticleSystem item in particlesSystem)
        {
            item.Play();
        }
    }   
    public void StopAll()
    {
        foreach (ParticleSystem item in particlesSystem)
        {
            item.Stop();
        }
    }
}
