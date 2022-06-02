using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(SilhouetteCanvas))]
public class SilhouetteTelephone : Singleton<SilhouetteTelephone>
{
    public List<Outcome> outcomes; 
    public List<SilhouetteData> silhouettes = new List<SilhouetteData>();
    public float onTimeResolved = 10f;
    public UnityEvent OnSilhouetteResolved;
    private SilhouetteCanvas canvas;
    [SerializeField]private int currentSilhouetteValidation;
    public int minSilhouetteValidation;
    public bool wasLastValidation = false;
    [SerializeField] private bool testBool = false;

    // Start is called before the first frame update
    void Start()
    {
        outcomes = new List<Outcome>();
        canvas = GetComponent<SilhouetteCanvas>();
        AddOutcome("test", 0);
        AddOutcome("test2", 1);
        AddOutcome("test3", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (testBool)
        {
            testBool = false;
            DisplayOutcomes();
        }
    }

    public void AddOutcome(string outcomeText, int id)
    {
        foreach (var outcome in outcomes)
        {
            if (outcome._outcomeId == id)
            {
                Debug.LogWarning("Outcome: " + outcomeText + " with id: " + id + " already exists");
                return;
            }
        }
        outcomes.Add(new Outcome(outcomeText,id));
        outcomes[outcomes.Count-1]._outcomeEvent.AddListener(delegate{ DisplaySilhouette(id);});     
        //ajoute x instance de displaySilhouette => a debugger   
    }

    //debug function to display all the silhouettes
    private void BroadAllCastSilhouette(){
        foreach (Outcome outcome in outcomes){
            foreach(SilhouetteData s in silhouettes){
                if(s.identity.id == outcome._outcomeId){
                    s.gameObject.SetActive(true);
                    s.identity.RecieveData(outcome);
                }
            }
        }
    }

    private void DisplaySilhouette(int id){
        foreach (SilhouetteData s in silhouettes){
            if(s.identity.id == id){
                s.gameObject.SetActive(true);
            }
            currentSilhouetteValidation++;
            CheckIfAllValidationAreDone();
        }
    }

    public void DisplayOutcomes(){
        silhouettes[silhouettes.Count-1].identity.isLastValidation = true;
        AddLeaveCondition();
        canvas.CreateNewCanvas(outcomes);
    }


    public void addSilhouetteData(GameObject self){
        self.TryGetComponent<SilhouetteData>(out SilhouetteData data);
        if(data.identity.id != -1){
            silhouettes.Add(data);
        }
        silhouettes.Sort((x,y)=> x.identity.id.CompareTo(y.identity.id)); // sort the list of silhouettes by id
    }

    private void AddLeaveCondition(){
        foreach(Outcome outcome in outcomes){
            foreach(SilhouetteData silhouette in silhouettes)
            {
                if(silhouette.identity.id == outcome._outcomeId && silhouette.identity.isLastValidation){
                    outcome._outcomeEvent.AddListener(delegate{ Resolved();});
                }
            }
        }
    }
    public void CheckIfAllValidationAreDone(){
        if(currentSilhouetteValidation >= minSilhouetteValidation && wasLastValidation == true){
            StartCoroutine(WaitForLastSilhouette());
        }
    }

    private void Resolved(){
        wasLastValidation = true;
    }

    IEnumerator WaitForLastSilhouette(){
        Debug.Log("WaitForLastSilhouette");
        yield return new WaitForSeconds(onTimeResolved);
        OnSilhouetteResolved.Invoke();
        Projection.Instance.goBackInOffice = true;
        Projection.Instance.enableTransition = true;
        Projection.Instance.isTransition = true;

    }
}

[Serializable]
public class Outcome{
    public string _outcomeText;
    public int _outcomeId;
    public UnityEvent _outcomeEvent;

    public Outcome(string outcomeText, int outcomeId){
        _outcomeText = outcomeText;
        _outcomeId = outcomeId;
        _outcomeEvent = new UnityEvent();
    }
    public Outcome(string outcomeText, int outcomeId, UnityEvent outcomeEvent){
        _outcomeText = outcomeText;
        _outcomeId = outcomeId;
        _outcomeEvent = outcomeEvent;
    }
    public void OnValidate() {
        _outcomeEvent.Invoke();
    }
}

[Serializable]
public class Silhouette{
    public int id;
    public string outcome;
    public bool isLastValidation;
    public Outcome outcomeLink;
    public void RecieveData(Outcome _outcome){
        outcomeLink = _outcome;
        outcome = _outcome._outcomeText;
    }
    public bool SendPresence(GameObject self){
        if(SilhouetteTelephone.Instance != null){
            SilhouetteTelephone.Instance.addSilhouetteData(self);
            return true;
        }
        Debug.LogError("SilhouetteTelephone is null");
        return false;
    }
}