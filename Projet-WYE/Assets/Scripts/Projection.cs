using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Projection : Singleton<Projection>
{
    public bool onEditor;

    [Header("Refs")]
    public Transform player;

    public List<ObjectIn> objectsToDissolve;
    [Space(5)]
    public bool enableTransition;
    public bool isTransition;
    [Range(0, 50)]
    public float transitionValue = 50f;
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

    [Header("Fade parameters")]
    [SerializeField, Tooltip("Hide UI at this value")] private float beginFadeOutAt;
    [SerializeField, Tooltip("Show UI at this value")] private float beginFadeInAt;

    public bool revealScene;

    // Start is called before the first frame update
    void Start()
    {
        //transitionShaders = Resources.LoadAll("Resources/Materials/M_"+ +".mat");
        if (!onEditor)
        {
            foreach (var item in objectsToDissolve)
            {
                for (int i = 0; i < item.objects.Count; i++)
                {
                    if (item.objects[i] != null)
                    {
                        item.objects[i].SetVector("_PlayerPos", player.position);

                        item.objects[i].SetFloat("_Dissolve", transitionValue);
                    }
                    else
                    {
                        Debug.Log(item.objects[i].name);
                    }
                }
            }

            hasProjted = false;
            StopCoroutine(WaitForVoid());
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (onEditor)
        {
            for (int obj = 0; obj < objectsToDissolve.Count; obj++)
            {
                for (int i = 0; i < objectsToDissolve[obj].objects.Count; i++)
                {
                    if (objectsToDissolve[obj].objects[i].HasProperty("_Dissolve"))
                    {
                        objectsToDissolve[obj].objects[i].SetFloat("_Dissolve", transitionValue);
                    }
                    else
                    {
                        Debug.Log(objectsToDissolve[obj].location + " have a missing reference at " + i);
                    }
                }
            }
        }
#endif
        else if (enableTransition && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            for (int obj = 0; obj < objectsToDissolve.Count; obj++)
            {
                for (int i = 0; i < objectsToDissolve[obj].objects.Count; i++)
                {
                    objectsToDissolve[obj].objects[i].SetFloat("_Dissolve", transitionValue);
                }
            }
        }
        else if (enableTransition && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            for (int obj = 0; obj < objectsToDissolve.Count; obj++)
            {
                for (int i = 0; i < objectsToDissolve[obj].objects.Count; i++)
                {
                    objectsToDissolve[obj].objects[i].SetFloat("_Dissolve", transitionValue);
                }
            }
        }
        else if (enableTransition && MasterManager.Instance.currentPhase == Phases.Phase_0)
        {
            if (enableTransition && TutoManager.Instance.firstPartIsDone && !TutoManager.Instance.secondPartIsDone)
            {
                for (int obj = 0; obj < 1; obj++)
                {
                    for (int i = 0; i < objectsToDissolve[obj].objects.Count; i++)
                    {
                        objectsToDissolve[obj].objects[i].SetFloat("_Dissolve", transitionValue);
                    }
                }
            }
            else if (enableTransition && TutoManager.Instance.secondPartIsDone && MasterManager.Instance.currentPhase == Phases.Phase_0)
            {
                for (int obj = 2; obj < 2; obj++)
                {
                    for (int i = 0; i < objectsToDissolve[obj].objects.Count; i++)
                    {
                        objectsToDissolve[obj].objects[i].SetFloat("_Dissolve", transitionValue);
                    }
                }
            }
        }

        if (pauseBetweenTransition && isTransition)
        {
            Deconstruct();
        }

        if (revealScene && !isTransition && enableTransition)
        {
            RevealScene();
        }

        if (transitionValue <= beginFadeOutAt && UIManager.Instance != null)
        {
            UIManager.Instance.Fade(Fadetype.Out);

            for (int i = 0; i < UnitManager.Instance.physicsbuttons.Count; i++)
            {
                UIManager.Instance.Fade(Fadetype.Out, UnitManager.Instance.physicsbuttons[i].icon);
            }
        }

        if (transitionValue >= beginFadeInAt && UIManager.Instance != null)
        {
            UIManager.Instance.Fade(Fadetype.In);

            for (int i = 0; i < UnitManager.Instance.physicsbuttons.Count; i++)
            {
                UIManager.Instance.Fade(Fadetype.In, UnitManager.Instance.physicsbuttons[i].icon);
            }
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

    public void SetTransitionValue(int value)
    {
        foreach (var item in objectsToDissolve)
        {
            for (int i = 0; i < item.objects.Count; i++)
            {
                item.objects[i].SetFloat("_Dissolve", value);
            }
        }
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
        if (transitionValue >= 0)
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
            StartCoroutine(WaitForVoid());
            CallScene();
            ToggleProjection();
        }
    }

    public void InTransition(bool value) { isTransition = value; }

    public void RevealScene()
    {
        if (transitionValue < 50f)
        {
            transitionValue += Time.deltaTime * time;

            if (transitionValue >= 50f)
            {
                revealScene = false;
                hasProjted = false;
                hasCycle = false; 
            }
        }
    }

    public void Construct()
    {
        if (transitionValue < 15)
        {
            transitionValue += Time.deltaTime * time;
        }
        else if (hasCycle)
        {
            hasCycle = false;
            isTransition = false;
            transitionValue = 15;
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
        if (!revealScene && (MasterManager.Instance.currentPhase == Phases.Phase_1 || TutoManager.Instance.isTutoDone) && !OrderController.Instance.isResolve)
        {
            Debug.Log("Imaginary");

            hasCycle = !false;

            MasterManager.Instance.isInImaginary = true;

            switch (ScenarioManager.Instance.currentScenario)
            {
                case Scenario.TrappedMan:
                    MasterManager.Instance.ChangeSceneByName(2, "TrappedMan");
                    break;
                case Scenario.HomeInvasion:
                    MasterManager.Instance.ChangeSceneByName(2, "HomeInvasion"); 
                    break;
                case Scenario.RisingWater:
                    MasterManager.Instance.ChangeSceneByName(2, "RisingWater");
                    break;
            }

        }

        if (goBackInOffice && OrderController.Instance.isResolve && MasterManager.Instance.currentPhase == Phases.Phase_2)
        {
            Debug.Log("Going back to Office");
            hasCycle = !false;

            MasterManager.Instance.isInImaginary = false;

            MasterManager.Instance.ChangeSceneByName(3, "Office");
        }


        if (!TutoManager.Instance.isTutoDone && MasterManager.Instance.currentPhase == Phases.Phase_0 && TutoManager.Instance.firstPartIsDone)
        {
            Debug.Log("Tuto Scene 2");
            InitTutorial.Instance.DisableObject();
            SceneLoader.Instance.AddNewScene("TutoScene_Two");
            TutoManager.Instance.Progress(12);
        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_0 && TutoManager.Instance.isTutoDone && !MasterManager.Instance.isInImaginary)
        {
            Debug.Log("Menu");

            MasterManager.Instance.Reset();
            SceneLoader.Instance.Unload("TutoScene");
            SceneLoader.Instance.Unload("TutoScene_Two");
            MasterManager.Instance.ChangeSceneByName(0, "Menu");
        }
    }

    /// <summary>
    /// This method is for the prototype of the sequence order phase (Phase 3)
    /// Will have to be move and re-write if needed.
    /// </summary>
    public void CallSequenceScene()
    {
        switch (ScenarioManager.Instance.currentScenario)
        {
            case Scenario.TrappedMan:
                //MasterManager.Instance.AddSceneByName(3, "");
                break;
            case Scenario.HomeInvasion:
                MasterManager.Instance.AddSceneByName(3, "Phase3_Prototype_Test");
                break;
            case Scenario.RisingWater:
                //MasterManager.Instance.AddSceneByName(3, "");
                break;
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
        Tutoriel,
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
