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

    public float timer;
    public float setTimer;
    public bool changeScene;
    public bool goToOffice;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.position;
        timer = setTimer;

        foreach (var mat in transitionShaders)
        {
            mat.SetVector("_PlayerPos", playerPos);
            mat.SetFloat("_Distance", 3f * 10f);
        }

        if (goToOffice)
        {
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

        if (MasterManager.Instance.isInImaginary)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                goToOffice = true;
                timer = 0;
                changeScene = true;

                DoTransition(0);
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
                changeScene = true;

                if (changeScene && goToOffice)
                {
                    startTransition = false;
                    MasterManager.Instance.GoBackToScene("Office");
                } 
                else if (changeScene)
                {
                    changeScene = false;
                    startTransition = false;
                    MasterManager.Instance.ActivateImaginary("Call1");
                }

                transitionValue = 1;
                range = 0;
            }
        }
        else if (state == 1)
        {
            range += time *Time.deltaTime;

            if (range >= 2.5)
            {
                startTransition = false;
                transitionValue = 0;

                range = 3;

                goToOffice = !goToOffice;
            }
        }
    }

    public void ResetTransition()
    {

    }
}
