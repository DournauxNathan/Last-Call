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

    [SerializeField] public bool isTransition /*{ get; set;}*/ = true;
    [SerializeField] public bool sTransition /*{ get; set; } */= false;

    private bool hasCycle = false;

    [SerializeField] private bool hasProjted;
    [SerializeField] private bool isDisconstruc;



    // Start is called before the first frame update
    void Start()
    {
        foreach (var mat in transitionShaders)
        {
            mat.SetFloat("_Distance", 3f * 10f);
        }

        timer = timeBetweenEachTransition;
        hasProjted = false;
        StopCoroutine(WaitForVoid());
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



        if (sTransition && isTransition && !isDisconstruc)
        {
            Deconstruct();
        }

        if (sTransition && isTransition && isDisconstruc)
        {
            Construct();
        }



    }

   /* public void DoTransition(int state)
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
    }*/

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
            isDisconstruc = true;
            StartCoroutine(WaitForVoid());//coroutine
            CallScene();
            ToggleProjted();
            


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
            isDisconstruc = false;
        }
        else
        {
            range = 3;

            ToggleProjted();
            StartCoroutine(WaitForVoid());//coroutine
            CallScene();
           


        }
    }

    public void CallScene()
    {
        if (!hasCycle && !hasProjted)
        {
            hasCycle = !false;

            MasterManager.Instance.ActivateImaginary("Gameplay_Combination_Iteration"); // A changer avec le scenario Manager quand plusier senar 
            

        }

        if (!hasCycle && hasProjted)
        {
            hasCycle = !false;

            MasterManager.Instance.GoBackToOffice("Office");
            

        }

        
    }

    public void ToggleProjted() 
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

        sTransition = false;
        Debug.Log("Wait for Void");
        yield return new WaitForSeconds(1f);
        sTransition = true;
    }

}
