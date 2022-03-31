using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class InspectionInWorld : Singleton<InspectionInWorld>
{
    [Header("Param")]
    public Transform _containers;
    public List<string> _listString;
    [Range(0.1f, 1f)] public float minRand = 0.1f;
    [Range(0.1f, 1f)] public float maxRand = 1f;

    [Header("Prefabs")]
    public GameObject textPrefab;

    [Header("Debug")]
    [SerializeField] private bool hascreatedText = false;
    [SerializeField] private bool clearBool = false;
    [SerializeField] private string testString;
    [SerializeField] private List<Animator> animators;

    // Start is called before the first frame update
    void Start()
    {

        _listString = new List<string>(); //INIT
        animators = new List<Animator>(); //INIT
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


        if (animators.Count != 0 && animators[0].GetCurrentAnimatorStateInfo(0).IsName("InspectionDisapeared"))
        {
            //Debug.Log("Test Anime");
            for (int i = 0; i < _containers.childCount; i++)
            {
                Destroy(_containers.GetChild(i).gameObject);
                Debug.Log("Cleared Child(" + i + "):" + _containers.GetChild(i).GetComponentInChildren<TMP_Text>().text);

            }
            animators.Clear();

        }

    }



    public void CreateNewText(string _text)
    {
        hascreatedText = false;
        if (_text != string.Empty)
        {
            

            Instantiate(textPrefab, _containers);
            int _nbchilds = _containers.childCount;
            _containers.GetChild(_nbchilds - 1).GetComponentInChildren<TMP_Text>().text = _text;
            var _tanim = _containers.GetChild(_nbchilds - 1).GetComponentInChildren<Animator>();
            _tanim.speed = Random.Range(minRand, maxRand);
            animators.Add(_tanim);
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
                _containers.GetChild(_nbchilds - 1).GetComponentInChildren<TMP_Text>().text = text;
                var _tanim = _containers.GetChild(_nbchilds - 1).GetComponentInChildren<Animator>();
                _tanim.speed = Random.Range(minRand, maxRand);
                animators.Add(_tanim);

            }

        }
    }


    public void ClearAllText()
    {
        clearBool = false;
        foreach (var animator in animators)
        {
            animator.SetTrigger("Disapeared");
        }
    }

    
}
