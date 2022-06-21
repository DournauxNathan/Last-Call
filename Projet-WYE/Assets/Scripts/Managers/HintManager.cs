using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.Events;


public class HintManager : Singleton<HintManager>
{
    public float timeBlinking;
    public Color32 color;
    public List<HintInWorld> hints = new List<HintInWorld>();
    public List<HintCanvasBehavior> currentHintCanvas = new List<HintCanvasBehavior>();
    public HintCanvasBehavior hintPrefab;
    public float timeBetweenHints = 120f;
    [SerializeField]private bool testBool = false;
    private int precombi;

    private void Start() {
        //DisplayHint(0); DEBUG ONLY
        StartCoroutine(HintsIfTooMuchTime());
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
            if (hint.id == id && CheckIfObjectIsdiabled(hint.attatchedTo))
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
    public void DisplayHint(int id, out bool isDisplayed){
        foreach (HintInWorld hint in hints)
        {
            if (hint.id == id && CheckIfObjectIsdiabled(hint.attatchedTo))
            {
                if(hint.attatchedTo == null){
                    Debug.LogError("Hint: " + hint + " with id: " + hint.id + " is not attatched to anything");
                    isDisplayed = false;
                    return; 
                }
                HintCanvasBehavior newHint = Instantiate(hintPrefab, hint.attatchedTo.transform); //Set parent to the object that has the hint
                currentHintCanvas.Add(newHint);
                SetValues(newHint, hint);
                isDisplayed = true;
                return; 
            }
        }
        Debug.LogWarning("Hint: " + id + " does not exist");
        isDisplayed = false; 
    }



    public void DisplayHint(GameObject obj){
        foreach (HintInWorld hint in hints)
        {
            if (hint.attatchedTo == obj)
            {
                HintCanvasBehavior newHint = Instantiate(hintPrefab, obj.transform); //Set parent to the object that has the hint
                currentHintCanvas.Add(newHint);
                SetValues(newHint, hint);
                return;
            }
        }
        Debug.LogWarning("Hint: " + obj + " does not exist");
    }


    private void SetValues(HintCanvasBehavior hint, HintInWorld hintInWorld){
        if(hintInWorld.hintText == null || hintInWorld.attatchedTo == null || hintInWorld.duration <= 0){
            Debug.LogError("Hint: " + hintInWorld.id + " has a problem" + "\n"+
                           "HintText: " + hintInWorld.hintText + "\n" +
                           "AttatchedTo: " + hintInWorld.attatchedTo + "\n" +
                           "Duration: " + hintInWorld.duration+ "\n"+
                           "has not been set up correctly");
            Destroy(hint.gameObject);
            return;
        }
        if(hintInWorld.attatchedTo.transform.childCount>1){
            Destroy(hint.gameObject);
            Debug.LogWarning("Hint: " + hintInWorld.id + " is alredy attatched to a child of " + hintInWorld.attatchedTo + " and will not be displayed");
            currentHintCanvas.Remove(hint);
            return;
        } 
        hint._hintInWorldData = hintInWorld; //reference to the hint in the world
        hint.SetText(hintInWorld.hintText);
        hint.transform.position = new Vector3(hint._hintInWorldData.attatchedTo.transform.localPosition.x, hint._hintInWorldData.attatchedTo.transform.localPosition.y + hint._hintInWorldData.offset, hint._hintInWorldData.attatchedTo.transform.localPosition.z); //Set position to the object that has the hint + offset
        //hint._hintInWorldData.OnhintDisappear.RemoveListener(delegate { DestroyHint(hint); });
        hint._hintInWorldData.OnhintDisappearAction += ()=> {DestroyHint(hint);};
        StartCoroutine(hint._hintInWorldData.DisplayHint());
        if(hint._hintInWorldData.hintSound!=null) hint.PlaySound(hint._hintInWorldData.hintSound); //Play sound if there is one (Spatialised)
        SetOutlineBlink(hint);
    }

    private void SetOutlineBlink(HintCanvasBehavior hint){
        if(hint._hintInWorldData.attatchedTo.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer)){
            Outline _outline = null;
            OutlineTicTac _outlineTicTac = null;
            if(!hint._hintInWorldData.attatchedTo.TryGetComponent<Outline>(out _outline)){
                _outline = hint._hintInWorldData.attatchedTo.AddComponent<Outline>();
            }
            _outline.OutlineColor = color;
            if(!hint._hintInWorldData.attatchedTo.TryGetComponent<OutlineTicTac>(out _outlineTicTac)){
                _outlineTicTac = hint._hintInWorldData.attatchedTo.AddComponent<OutlineTicTac>();
            }
            _outlineTicTac.delay = timeBlinking;
            _outlineTicTac.ToggleTicTac();
        }
    }


    private void DestroyHint(HintCanvasBehavior hint){
        //Debug.Log("nb_Event");
        if (hint == null){ /*hint._hintInWorldData.OnhintDisappear.RemoveListener( delegate { DestroyHint(hint); });*/ return;}
        if(hint._hintInWorldData.attatchedTo.TryGetComponent<Outline>(out Outline outline) && hint._hintInWorldData.attatchedTo.TryGetComponent<OutlineTicTac>(out OutlineTicTac outlineTicTac)){
            Destroy(outlineTicTac);
            outline.enabled = false;
        }
        currentHintCanvas.Remove(hint);
        Destroy(hint.gameObject);
        StopCoroutine(hint._hintInWorldData.DisplayHint());
        hint._hintInWorldData.OnhintDisappearAction = null;
    }

    private bool CheckIfObjectIsdiabled(GameObject obj){
        if(obj.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer)){
            return meshRenderer.enabled;
        }
        else{
        Debug.LogWarning("Object: " + obj + " does not have a MeshRenderer");
        return false;
        }
    }

    IEnumerator HintsIfTooMuchTime(){
        precombi = OrderController.Instance.puzzlesSucced; // Save the current to chek if you need to display a new int
        bool hasDisplay = true;
        while(hints.Count > 0){
            yield return new WaitForSeconds(timeBetweenHints);
            if(precombi == OrderController.Instance.puzzlesSucced){
                DisplayHint(0,out hasDisplay);
                if(!hasDisplay){
                    while(hints.Count>0 && !hasDisplay){
                        hints.RemoveAt(0);
                        DisplayHint(0,out hasDisplay);
                    }
                }
                precombi = OrderController.Instance.puzzlesSucced;
            }
            else{
                precombi = OrderController.Instance.puzzlesSucced;
            }
        }
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

    public HintInWorld(GameObject obj,string _text,AudioClip _clip,float _offset,int _id,float _duration){
        attatchedTo = obj;
        hintText = _text;
        hintSound = _clip;
        offset = _offset;
        id = _id;
        duration = _duration;
        OnhintDisplay = new UnityEvent();
        OnhintDisappear = new UnityEvent();
        OnhintDisappearAction = null;
    }   
}