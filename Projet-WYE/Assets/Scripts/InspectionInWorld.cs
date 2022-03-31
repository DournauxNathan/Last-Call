using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectionInWorld : MonoBehaviour
{
    [Header("Param")]
    public Transform _containers;
    public List<string> _listString;

    [Header("Prefabs")]
    public GameObject textPrefab;

    [Header("Debug")]
    [SerializeField] private int toInstantiateTest;
    [SerializeField] private bool hascreatedText = false;
    [SerializeField] private string testString;

    

    // Start is called before the first frame update
    void Start()
    {
        Debuggg();
        _listString = new List<string>(); //INIT
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hascreatedText)
        {
            CreateNewText(testString);
            CreateNewText(_listString);
        }
    }

    private void Debuggg()
    {
        if (toInstantiateTest != 0)
        {
            for (int i = 0; i < toInstantiateTest; i++)
            {
                Instantiate(textPrefab, _containers);
            }
        }
    }

    private void CreateNewText(string _text)
    {
        if (_text != string.Empty)
        {
            hascreatedText = false;
            Instantiate(textPrefab, _containers);
            int _nbchilds = _containers.childCount;
            _containers.GetChild(_nbchilds - 1).GetComponent<TMP_Text>().text = _text;
        }
    }

    private void CreateNewText(List<string> _listText)
    {
        hascreatedText = false;
        foreach (var text in _listText)
        {
            if (text != string.Empty)
            {
                Instantiate(textPrefab, _containers);
                int _nbchilds = _containers.childCount;
                _containers.GetChild(_nbchilds - 1).GetComponent<TMP_Text>().text = text;
            }
            
        }
    }

}
