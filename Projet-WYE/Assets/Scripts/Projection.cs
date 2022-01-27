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
    private Vector3 playerPos;

    [Header("Parameters for testing the go/back mecanics")]
    public float timeBetweenEachTransition;
    public float timer;
    public bool changeScene;
    public bool goBackInOffice;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Distance", 3f * 10f);
        }

        timer = timeBetweenEachTransition;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.position;

        foreach (var mat in transitionShaders)
        {
            mat.SetVector("_PlayerPos", playerPos);
        }

        if ((MasterManager.Instance.canImagine && startTransition) || startTransition)
        {
            DoTransition(transitionValue);
        }

        if (MasterManager.Instance.isInImaginary)
        {
            timer -= Time.deltaTime;
            goBackInOffice = true;

            if (timer <= 0)
            {
                timer = 0;
                DoTransition(0);
            }
        }

        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Distance", range * 10f);
        }         
    }

    public void DoTransition(int state)
    {
        if (state == 0)
        {
            range -= time * Time.deltaTime;

            if (range <= 0)
            {
                startTransition = true;
                transitionValue = 1;
            
                range = 0;

                if (changeScene && goBackInOffice)
                {
                    goBackInOffice = false;
                    MasterManager.Instance.GoBackToOffice("Office");
                    goBackInOffice = false;
                }

                if (changeScene)
                {                   
                    MasterManager.Instance.ActivateImaginary("Call1");
                }
            }
        }
        else if (state == 1)
        {
            range += time *Time.deltaTime;

            if (range >= 2.5)
            {
                startTransition = true;
                transitionValue = 0;
                range = 3;
                
            }
        }
    }
}
