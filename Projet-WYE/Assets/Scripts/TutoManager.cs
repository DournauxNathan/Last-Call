using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> advice;
    [SerializeField] private int progresion = 0;
    [SerializeField] private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        if (FileHandler.IsAFileExist("SaveLastCall.json"))
        {
           // SkipTuto(); //To Delete after testing
        }
        RefreshAdvice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Progress()
    {
        progresion++;
        RefreshAdvice();
    }

    public void DisplayMore(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void ChangeTextTarget(string text)
    {
        _text.text = text;
    }
    public void DeleteTarget(GameObject target)
    {
        Destroy(target);
    }

    private void RefreshAdvice()
    {
        foreach (var adv in advice)
        {
            adv.SetActive(false);
        }
        if (progresion <= advice.Count )
        {
            advice[progresion].SetActive(true);
        }
    }

    public void SkipTuto()
    {
        SceneLoader.Instance.LoadNewScene("Menu");
        MasterManager.Instance.currentPhase = Phases.Phase_0;
    }

}
