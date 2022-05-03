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
        advice[progresion].SetActive(true);
    }



}
