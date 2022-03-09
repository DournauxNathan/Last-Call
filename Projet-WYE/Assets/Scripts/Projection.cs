using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projection : Singleton<Projection>
{
    [Header("Refs")]
    public Transform player;

    [Space(5)] 
    public List<Material> transitionShaders;
    public List<Material> wallShader;
    [Space(5)]

    public bool isTransition;
    [Range(0, 3)]
    public float transitionValue = 3f;
    [Range(0, 3)]
    public float wallTransition = 3f;
    public bool setWallWithOutline = false;

    [Header("Projection Properties")]
    [Tooltip("During of the transition in seconds")]
    public float time;
    [SerializeField] private bool pauseBetweenTransition = true;
    [Tooltip("Time of the break between the Transition effect")]
    public float timeBetweenTransition;
    [Space(5)]
    public bool changeScene;
    public bool goBackInOffice;

    [Header("Cycle Properties")]
    public bool hasCycle = false;

    public bool hasProjted;
    private bool isDisconstruc;

    // Start is called before the first frame update
    void Start()
    {
        //transitionShaders = Resources.LoadAll("Resources/Materials/M_"+ +".mat")
        
        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Dissolve", 3f * 10f);
        }

        foreach (var mat in wallShader)
        {
           // mat.SetFloat("_Dissolve", 3f * 10f);
        }

        hasProjted = false;
        StopCoroutine(WaitForVoid());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var mat in transitionShaders)
        {
            mat.SetVector("_PlayerPos", player.position);
        
            mat.SetFloat("_Dissolve", transitionValue * 10f);
        }

        foreach (var mat in wallShader)
        {
            //mat.SetFloat("_Dissolve", transitionValue * 10f);
        }


        if (pauseBetweenTransition && isTransition && !isDisconstruc)
        {
            Deconstruct();
        }

        if (pauseBetweenTransition && isTransition && isDisconstruc)
        {
            Construct();
        }

        if (transitionValue >= 2.5)
        {
            foreach (var mat in wallShader)
            {
                DistanceDissolveTarget.Instance.SetObjectToTrack();
            }
        }
    }

    public void ResetTransition()
    {
        if (transitionValue < 3)
        {
            isTransition = false;
            transitionValue += Time.deltaTime * time;

            if (transitionValue > 3)
            {
                transitionValue = 3;
            }
        }
    }

    public void Deconstruct()
    {
        if (transitionValue > 0)
        {
            isTransition = true;
            transitionValue -= Time.deltaTime * time;
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            transitionValue = 0;
        }
        else
        {
            transitionValue = 0;
            isDisconstruc = true;
            StartCoroutine(WaitForVoid());//coroutine
            CallScene();
            ToggleProjection();
        }
    }

    public void Construct()
    {
        if (transitionValue < 3)
        {
            isTransition = true;
            transitionValue += Time.deltaTime * time;
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            transitionValue = 3;
            isDisconstruc = false;
        }
        else
        {
            transitionValue = 3;

            ToggleProjection();
            StartCoroutine(WaitForVoid());//coroutine
            CallScene();
        }
    }

    public void CallScene()
    {
        if (!hasCycle && !hasProjted)
        {
            hasCycle = !false;

            MasterManager.Instance.isInImaginary = true;
            MasterManager.Instance.ActivateImaginary("Gameplay_Combination_Iteration"); // A changer avec le scenario Manager quand plusier senar 
        }

        if (!hasCycle && hasProjted)
        {
            hasCycle = !false;

            //MasterManager.Instance.isInImaginary = false;
                        
            MasterManager.Instance.currentPhase = Phases.Phase_3;

            MasterManager.Instance.GoBackToOffice("Office");
        }
    }

    public void ToggleProjection() 
    {
        if (hasProjted)
        {
            hasProjted = false;
        }
        else
        {
            hasProjted = true;
        }
    }

    IEnumerator WaitForVoid()
    {
        pauseBetweenTransition = false;
        yield return new WaitForSeconds(timeBetweenTransition);
        pauseBetweenTransition = true;
    }

}
