using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.Events;


public class HintManager : Singleton<HintManager>
{
    public List<HintInWorld> hints = new List<HintInWorld>();
    public List<HintCanvasBehavior> currentHintCanvas = new List<HintCanvasBehavior>();
    public HintCanvasBehavior hintPrefab;
    [SerializeField]private bool testBool = false;

    private void Start() {
        //DisplayHint(0); DEBUG ONLY
    }

    private void Update() {
        if(testBool){
            testBool = false;
            foreach(HintInWorld hint in hints){
                DisplayHint(hint.id);
            }
        }
    }

    public void DisplayHint(int id){
        foreach (HintInWorld hint in hints)
        {
            if (hint.id == id)
            {
                if(hint.attatchedTo == null){
                    Debug.LogError("Hint: " + hint + " with id: " + hint.id + " is not attatched to anything");
                    return;
                }
                HintCanvasBehavior newHint = Instantiate(hintPrefab, hint.attatchedTo.transform); //Set parent to the object that has the hint
                currentHintCanvas.Add(newHint);
                SetValues(newHint, hint);
                return;
            }
        }
        Debug.LogWarning("Hint: " + id + " does not exist");
    }

    private void SetValues(HintCanvasBehavior hint, HintInWorld hintInWorld){
        if(hintInWorld.hintText == null || hintInWorld.attatchedTo == null || hintInWorld.duration == 0){
            Debug.LogError("Hint: " + hintInWorld.id + " has a problem" + "\n"+
                           "HintText: " + hintInWorld.hintText + "\n" +
                           "AttatchedTo: " + hintInWorld.attatchedTo + "\n" +
                           "Duration: " + hintInWorld.duration+ "\n"+
                           "has not been set up correctly");
            Destroy(hint.gameObject);
            return;
        }
        hint._hintInWorldData = hintInWorld; //reference to the hint in the world
        hint.SetText(hintInWorld.hintText);
        hint.transform.position = new Vector3(hint._hintInWorldData.attatchedTo.transform.localPosition.x, hint._hintInWorldData.attatchedTo.transform.localPosition.y + hint._hintInWorldData.offset, hint._hintInWorldData.attatchedTo.transform.localPosition.z); //Set position to the object that has the hint + offset
        //hint._hintInWorldData.OnhintDisappear.RemoveListener(delegate { DestroyHint(hint); });
        hint._hintInWorldData.OnhintDisappearAction += ()=> {DestroyHint(hint);};
        StartCoroutine(hint._hintInWorldData.DisplayHint());
        if(hint._hintInWorldData.hintSound!=null) hint.PlaySound(hint._hintInWorldData.hintSound); //Play sound if there is one (Spatialised)
    }

    private void DestroyHint(HintCanvasBehavior hint){
        //Debug.Log("nb_Event");
        if (hint == null){ /*hint._hintInWorldData.OnhintDisappear.RemoveListener( delegate { DestroyHint(hint); });*/ return;}
        currentHintCanvas.Remove(hint);
        Destroy(hint.gameObject);
        hint._hintInWorldData.OnhintDisappearAction = null;
    }

}
[Serializable]
public class HintInWorld{
    public GameObject attatchedTo;
    public string hintText;
    public AudioClip hintSound; 
    public float offset;
    public int id;
    public float duration;
    public UnityEvent OnhintDisplay;
    public UnityEvent OnhintDisappear;

    public UnityAction OnhintDisappearAction;

    public Coroutine coroutine;
    public IEnumerator DisplayHint(){
        //Debug.Log("Coroutine started");
        OnhintDisplay.Invoke();
        yield return new WaitForSeconds(duration);
        OnhintDisappear.Invoke();
        OnhintDisappearAction?.Invoke();
    }
    
}