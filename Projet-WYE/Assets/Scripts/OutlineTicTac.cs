using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineTicTac : MonoBehaviour
{
    private Outline outline;
    private Coroutine coroutine;
    public float delay = 0.1f;
    public bool isTicTacing = false; // change the state
    public bool testBool = false; // debug only
    private bool state = false; // debug only
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Outline>(out outline);
        if(outline == null) Debug.LogError("OutlineTicTac: Outline component not found");
    }

    private void OnEnable() {
        TryGetComponent<Outline>(out outline);
        if(outline == null) Debug.LogError("OutlineTicTac: Outline component not found");
    }

    // Update is called once per frame
    void Update()
    {
        if(testBool && !state){
            testBool = false;
            state = true;
            ToggleTicTac();

        }
        else if(testBool && state){
            testBool = false;
            state = false;
            ToggleTicTac();
        }
    }

    public void ToggleTicTac(){
        isTicTacing = isTicTacing ? false : true;
        if(coroutine == null) coroutine = StartCoroutine(TicTac());
    }

    IEnumerator TicTac(){
        while(isTicTacing){
            outline.enabled = true;
            yield return new WaitForSeconds(delay);
            outline.enabled = false;
            yield return new WaitForSeconds(delay);
        }
        coroutine = null;
    }

}
