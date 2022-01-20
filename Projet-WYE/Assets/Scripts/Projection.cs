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

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.position;

        foreach (var mat in transitionShaders)
        {
            mat.SetVector("_PlayerPos", playerPos);
            mat.SetFloat("_Distance", 3f * 10f);
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
    }

    public void DoTransition(int state)
    {
        if (state == 0)
        {
            range -= time * Time.deltaTime;

            if (range <= 0)
            {
                startTransition = false;
                transitionValue = 1;
            
                range = 0;
                MasterManager.Instance.ActivateImaginary("Call1");
            }
        }
        else if (state == 1)
        {
            range += time *Time.deltaTime;

            if (range >= 2.5)
            {
                startTransition = false;
                transitionValue = 0;
                range = 5;
                
            }
        }
    }
}
