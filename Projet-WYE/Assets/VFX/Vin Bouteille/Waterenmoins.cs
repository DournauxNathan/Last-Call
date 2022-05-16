using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterenmoins : MonoBehaviour
{
    private Material Vin;
    public float descente = 1;

    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        Vin = GetComponent<Renderer>().material;
        Vin.SetFloat("_Descente", descente);
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            StartCoroutine(ExampleCoroutine());


        }

        Vin.SetFloat("_Descente", descente);
        if (test == false )
        {
            descente -= 0f;



        }
    }
    

   
    IEnumerator ExampleCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.

        descente -= 0.001f;
        yield return new WaitForSeconds(1);
    }
}
