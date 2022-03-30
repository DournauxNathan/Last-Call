using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectionInWorld : Singleton<InspectionInWorld>
{
    [Header("Param")]
    public Transform _containers;
    public List<string> _listString;

    [Header("Prefabs")]
    public GameObject textPrefab;

    [Header("Debug")]
    [SerializeField] private bool hascreatedText = false;
    [SerializeField] private bool clearBool = false;
    [SerializeField] private string testString;

    // Start is called before the first frame update
    void Start()
    {

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

        if (clearBool)
        {
            ClearAllText();
        }
    }



    public void CreateNewText(string _text)
    {
        hascreatedText = false;
        if (_text != string.Empty)
        {
            
            Instantiate(textPrefab, _containers);
            int _nbchilds = _containers.childCount;
            _containers.GetChild(_nbchilds - 1).GetComponent<TMP_Text>().text = _text;
        }
    }

    public void CreateNewText(List<string> _listText)
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

    public void ClearAllText()
    {
        for (int i = 0; i < _containers.childCount; i++)
        {
            Destroy(_containers.GetChild(i).gameObject);
            Debug.Log("Cleared Child(" + i + "):" + _containers.GetChild(i).GetComponent<TMP_Text>().text);
        }
        clearBool = false;
    }

    
}
