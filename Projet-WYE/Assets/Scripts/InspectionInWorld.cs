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
    [Range(0.1f, 1f)] public float minRand = 0.3001f;
    [Range(0.1f, 1f)] public float maxRand = 0.6401f;
    public List<Vector2> _listPlace;

    [Header("Prefabs")]
    public GameObject textPrefab;

    [Header("Debug")]
    [SerializeField] private bool hascreatedText = false;
    [SerializeField] private bool clearBool = false;
    [SerializeField] private string testString;
    [SerializeField] private List<Animator> animators;

    private List<string> _queueString;
    private Coroutine _generateTextCoroutine;


    // Start is called before the first frame update
    void Start()
    {

        _listString = new List<string>(); //INIT
        animators = new List<Animator>(); //INIT
        _queueString = new List<string>(); //INIT

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

    public void CreateNewText(List<string> _listText, float delay, bool hasRandom)
    {
        hascreatedText = false;
        var _listIndex = new List<int>(new int[] { 1, 4 });
        _queueString.Clear();
        _queueString.AddRange(_listText);
                

        switch(_listText.Count) //switch on the number of text
        {
            case 0:
                Debug.LogError("No text to display");
                break;
            case 2:
                _listIndex = new List<int>(new int[] { 1, 4 });
                break;
            case 4:
                _listIndex = new List<int>(new int[] { 0, 2, 3, 5 });
                break;
            case 6:
                _listIndex = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
                break;
            default:
                Debug.Log("Error: Number of text is not correct, please check the number of text in the list");
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
                break;
        }

        if(_generateTextCoroutine == null)
        {
            _generateTextCoroutine = StartCoroutine(GenerateText(_queueString, delay, _listIndex, hasRandom));
        }
    }

    IEnumerator GenerateText(List<string> _listText, float delay,List<int> _listPlacePos, bool hasRandom)
    {
        while (_listText.Count >0)
        {
            Debug.Log("Iterration " + _listText.Count);
            var i = Instantiate(textPrefab, _containers);
            int _nbchilds = _containers.childCount;
            _containers.GetChild(_nbchilds - 1).GetComponentInChildren<TMP_Text>().text = _listText[_listText.Count-1];
            (_containers.GetChild(_nbchilds - 1).transform as RectTransform).anchoredPosition = new Vector3(_listPlace[_listPlacePos[_listText.Count-1]].x, _listPlace[_listPlacePos[_listText.Count-1]].y, 0);
            if(hasRandom) (_containers.GetChild(_nbchilds-1).transform as RectTransform).anchoredPosition = new Vector3(_containers.GetChild(_nbchilds-1).localPosition.x+GetRandom(), _containers.GetChild(_nbchilds-1).localPosition.y+GetRandom(), 0);
            _listText.RemoveAt(_listText.Count - 1);
            //animText
            var _tanim = _containers.GetChild(_nbchilds - 1).GetComponentInChildren<Animator>();
            _tanim.speed = Random.Range(minRand, maxRand);
            animators.Add(_tanim);
            yield return new WaitForSeconds(delay);
        }

    }

    private float GetRandom()
    {
        float _minRand = minRand * 10f;
        float _maxRand = maxRand * 25f;
        return Random.Range(_minRand, _maxRand);
    }

    public void ClearAllText()
    {
        clearBool = false;
        foreach (var animator in animators)
        {
            animator.SetTrigger("Disapeared");
        }
    }

    public void StopGenerating(){
        StopCoroutine(_generateTextCoroutine);
    }

    
}
