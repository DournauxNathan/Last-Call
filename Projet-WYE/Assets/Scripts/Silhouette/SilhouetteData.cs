using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SilhouetteCanvas))]
public class SilhouetteData : MonoBehaviour
{
    public int id;
    public List<string> outcomes;
    private SilhouetteManager silhouetteManager;
    private Coroutine coroutine;
    [SerializeField]private bool testBool = false;

    private void Start() {
        //SetUp();
    }

    private void Update() {
        if(testBool){
            testBool = false;
            OnHoverEnter();
        }
    }

    public void AddOutcome(string outcome)
    {
        outcomes.Add(outcome);
    }
    public void AddOutcome(List<string> outcome)
    {
        outcomes.AddRange(outcome);
    }

    private void SetUp(){
        if(SilhouetteManager.Instance != null){
            silhouetteManager = SilhouetteManager.Instance;
            silhouetteManager.AddSilhouette(this);
        }  
        else{
            Debug.LogError("SilhouetteManager is null");
        }
    }

    private void OnEnable() {
        SetUp();
    }

// Set in XRGrab event OnHoverEnter
    public void OnHoverEnter()
    {
        if(coroutine != null) StopCoroutine(coroutine); coroutine = null;
        if(transform.childCount==0 && outcomes.Count>0){ //if there is no canvas yet and there is at least one outcome
            TryGetComponent<SilhouetteCanvas>(out SilhouetteCanvas canvas);
            canvas.CreateNewCanvas(this);
        }
    }

    //will be call when the user cancel a word otherwise words will stay in the world
    public void OnCancel()
    {
        if(coroutine == null) coroutine = coroutine = StartCoroutine(WaitForDisappear(silhouetteManager.timeToDisappear));
    }
    
    IEnumerator WaitForDisappear(float time)
    {
        yield return new WaitForSeconds(time);
        coroutine = null;
        TryGetComponent<SilhouetteCanvas>(out SilhouetteCanvas canvas);
        canvas.DestroyCanvas();
    }

    

}
