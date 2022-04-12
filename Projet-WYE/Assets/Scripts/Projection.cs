using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Projection : Singleton<Projection>
{
    [Header("Refs")]
    public Transform player;

    public List<ObjectIn> objectsToDissolve;
    [Space(5)]
    public bool enableTransition;
    public bool isTransition;
    [Range(0, 30)]
    public float transitionValue = 30f;
    [Range(0, 8)]
    public float wallTransition;
    public bool setWallWithOutline = false;
    public bool switchMode;

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

    [Header("Fade parameters")]
    [SerializeField, Tooltip("Hide UI at this value")] private float beginFadeOutAt;
    [SerializeField, Tooltip("Show UI at this value")] private float beginFadeInAt;


    // Start is called before the first frame update
    void Start()
    {
        //transitionShaders = Resources.LoadAll("Resources/Materials/M_"+ +".mat");
        
        foreach (var item in objectsToDissolve)
        {
            for (int i = 0; i < item.objects.Count; i++)
            {
                item.objects[i].SetVector("_PlayerPos", player.position);

                item.objects[i].SetFloat("_Dissolve", transitionValue);
            }
        }

        hasProjted = false;
        StopCoroutine(WaitForVoid());
    }

    // Update is called once per frame
    void Update()
    {
        if (enableTransition)
        {
            foreach (var item in objectsToDissolve)
            {
                for (int i = 0; i < item.objects.Count; i++)
                {
                    item.objects[i].SetFloat("_Dissolve", transitionValue);
                }
            }
        }

        if (pauseBetweenTransition && isTransition && !isDisconstruc)
        {
            Deconstruct();
        }


        if (transitionValue <= beginFadeOutAt)
        {
            UIManager.Instance.Fade(Fadetype.Out);
        }

        if (transitionValue >= beginFadeInAt)
        {
            UIManager.Instance.Fade(Fadetype.In);
        }


        /* if (pauseBetweenTransition && isTransition && isDisconstruc)
         {
             Construct();
         }

         if (transitionValue >= 2.5)
         {
             foreach (var mat in wallShader)
             {
                 //DistanceDissolveTarget.Instance.SetObjectToTrack();
             }
         }*/
    }

    public void ResetTransition()
    {
        if (transitionValue < 15)
        {
            isTransition = false;
            transitionValue += Time.deltaTime * time;

            if (transitionValue > 15)
            {
                transitionValue = 15;
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
            StartCoroutine(WaitForVoid());
            CallScene();
            ToggleProjection();
        }
    }

    public void Construct()
    {
        if (transitionValue < 15)
        {
            isTransition = true;
            transitionValue += Time.deltaTime * time;
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            transitionValue = 15;
            isDisconstruc = false;
        }
        else
        {
            transitionValue = 15;

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
            //Debug.Log("Call Gameplay_Combination_Iteration");
            MasterManager.Instance.ActivateImaginary("Gameplay_Combination_Iteration"); // A changer avec le scenario Manager quand plusieur senarios 
        }

        if (!hasCycle && hasProjted)
        {
            hasCycle = !false;

            MasterManager.Instance.isInImaginary = false;
                        
            MasterManager.Instance.currentPhase = Phases.Phase_3;

            //Debug.Log("Call Office");
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

    public enum Location
    {
        Office,
        Imaginary
    }

    [System.Serializable]
    public class ObjectIn
    {
        public Location location;
        public List<Material> objects;
    }

}
