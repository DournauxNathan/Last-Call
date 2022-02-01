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

    //Recode

    public bool isTransition = true;
    public bool sTransition = false;

    public bool hasCycle = false;


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

        /*if ((MasterManager.Instance.canImagine && startTransition) || startTransition)
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
        }*/

        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Distance", range * 10f);
        }



        if (sTransition && transitionValue == 0 && isTransition)
        {
            Deconstruct();
        }

        if (sTransition && transitionValue == 1 && isTransition)
        {
            Construct();
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
                    startTransition = false;
                    MasterManager.Instance.GoBackToOffice("Office");
                }

                if (changeScene)
                {
                    MasterManager.Instance.useOneInput = false;
                    MasterManager.Instance.ActivateImaginary("Gameplay_Combination_Iteration");
                }
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
                
            }
        }
    }

    public void Deconstruct()
    {
        if (range>0)
        {
            isTransition = true;
            range -= Time.deltaTime * time;
            Debug.Log("Deconstruct");
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            startTransition = false;
            range = 0;

        }
        else
        {
            range = 0;
            
            transitionValue = 1;
            CallScene();
            
        }


    }

    public void Construct()
    {
        if (range<3)
        {
            isTransition = true;
            range += Time.deltaTime * time;
            Debug.Log("Construc");
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            startTransition = false;
            range = 3;


        }

        else
        {
            Debug.Log(range);
            range = 3;

            
            transitionValue = 0;
            CallScene();
        }
    }

    public void CallScene()
    {
        if (!hasCycle && transitionValue == 1)
        {
            hasCycle = !false;

            MasterManager.Instance.ActivateImaginary("Gameplay_Combination_Iteration");
            

        }

        if (!hasCycle && transitionValue == 0)
        {
            hasCycle = !false;

            MasterManager.Instance.GoBackToOffice("Office");
            

        }
    }


}
