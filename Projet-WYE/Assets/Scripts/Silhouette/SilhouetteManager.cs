using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SilhouetteManager : Singleton<SilhouetteManager>
{
    public List<Silhouette> silhouettes = new List<Silhouette>();
    public float timeToDisappear = 4f;
    public int minSilhouetteValidation;
    private int currentSilhouetteValidation;
    public bool wasLastValidation = false;
    public UnityEvent OnSilhouetteResolve;
    void Start()
    {
        
    }
    public void AddSilhouette(SilhouetteData data)
    {
        foreach (Silhouette s in silhouettes)
        {
            if (s.id == data.id)
            {
                Debug.LogWarning("Silhouette: " +data.name + " with id: " + data.id + " already exists");
                return;
            }
        }
        silhouettes.Add(new Silhouette(data));
    }
    
    // add string to list of outcomes
    public void Addoutcome(int id,string outcome)
    {
        foreach (Silhouette s in silhouettes)
        {
            if (s.id == id)
            {
                //add to both, the list of outcomes and the list of outcomes of the silhouette
                s.outcomes.Add(outcome);
                s.silhouetteData.AddOutcome(outcome);
                return;
            }
        }

        Debug.LogWarning("Silhouette: " + id + " does not exist");
    }

    // add list of string to list of outcomes
    public void Addoutcome(int id,List<string> outcome)
    {
        foreach (Silhouette s in silhouettes)
        {
            if (s.id == id)
            {
                //add to both, the list of outcomes and the list of outcomes of the silhouette
                s.outcomes.AddRange(outcome);
                s.silhouetteData.AddOutcome(outcome);
                return;
            }
        }

        Debug.LogWarning("Silhouette: " + id + " does not exist");
    }

    public void CheckIfAllValidationAreDone()
    {
        if (currentSilhouetteValidation >= minSilhouetteValidation && wasLastValidation == true)
        {
            OnSilhouetteResolve?.Invoke(); 
            
            Debug.Log("Silhouette resolved");

            OrderController.Instance.isResolve = true;

            Projection.Instance.goBackInOffice = true;
            Projection.Instance.enableTransition = true;
            Projection.Instance.isTransition = true;
        }
    }
    public void IncreaseCurrentSilhouetteValidation(){
        currentSilhouetteValidation++;
    }
    public void LastValidation(bool value){
        wasLastValidation = value;
    }


}

[Serializable]
public class Silhouette{
    public string name;
    public int id;
    public List<string> outcomes = new List<string>();
    public SilhouetteData silhouetteData;

    public Silhouette(SilhouetteData data){
        this.name = data.name;
        this.id = data.id;
        this.silhouetteData = data;
    }
}
