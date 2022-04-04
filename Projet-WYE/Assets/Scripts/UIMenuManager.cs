using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Animations;

public class UIMenuManager : MonoBehaviour
{
   [SerializeField] private bool firstSetUp = false;

    //MainParam
    [Header("Param")]
    [Range(0.1f,1f)]public float animSpeed = 0.3f;
    [Header("Debug")]
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform wheel;[SerializeField] private Transform wheelParameters;
    [SerializeField]private List<Transform> wheelList;
    [SerializeField] private List<Transform> wheelParmetersList;
    [SerializeField] private Transform currentSelected;
    [SerializeField] private Transform oldSelected;
    [SerializeField] private List<Vector3> pos;

    [Header("Events")]
    [Space(10)] public UnityEvent StartGame;



    /*//Quality Param
    public enum Quality
    {
        Low,Medium,High
    }
    [Space(15)]
    [SerializeField] private Transform qualityButton;
    public Quality qualityChosen = Quality.Medium;*/

    void Start()
    {
        //EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        wheelList = new List<Transform>();
        SetUp();
        EventSystem.current.SetSelectedGameObject(wheelList[2].GetChild(0).gameObject);



    }

    // Update is called once per frame
    void Update()
    {

        StartGame.AddListener(EventCall);

        if (EventSystem.current.currentSelectedGameObject != null && currentSelected != EventSystem.current.currentSelectedGameObject.gameObject.transform)
        {
            oldSelected = currentSelected;
            OnWheelUpdate();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(wheelList[2].GetChild(0).gameObject);
        }


        CurrentSelected();


    }

    private void SetUp() 
    {
        wheelList.Clear();
        for (int i = 0; i < wheel.childCount; i++)
        {
            wheelList.Add(wheel.GetChild(i));
            pos.Add(wheel.GetChild(i).position); Debug.Log(wheel.GetChild(i).name);
        }
        if (wheelParmetersList.Count == 0)
        {
            for (int i = 0; i < wheelParameters.childCount; i++)
            {
                wheelParmetersList.Add(wheelParameters.GetChild(i));
                wheelParmetersList[i].gameObject.SetActive(false);
            }
            wheelParmetersList[2].gameObject.SetActive(true);
        }
        if (!firstSetUp)
        {
            firstSetUp = true;
            IsThereASave("SaveLastCall.json");

            //qualityButton.GetComponentInChildren<Text>().text = "Graphics: " + qualityChosen;
        }
    }

    private void OnWheelUpdate()
    {
        UpdateWheel(DirectionWheel());
    }

    private void CurrentSelected()
    {
        /*if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(oldSelected.gameObject);
        }*/
        currentSelected = EventSystem.current.currentSelectedGameObject.transform;
    }

    private int DirectionWheel()
    {

        if (EventSystem.current.currentSelectedGameObject.gameObject.transform == wheelList[1].GetChild(0))
        {
            return 1;
        }
        else if(EventSystem.current.currentSelectedGameObject.gameObject.transform == wheelList[3].GetChild(0))
        {
            return -1;
        }
        else
        {
            return 0;
        }
        
    }

    private void AffParam(string name)
    {
        name += "Param"; 
        Transform toenable = null;

        foreach (var param in wheelParmetersList)
        {
            param.gameObject.SetActive(false);
            if (param.name == name)
            {
                toenable = param;
            }                       
        }
        if (toenable!=null)
        {
            toenable.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("display is null");
        }
    }

    private void UpdateWheel(int index) 
    {

        if (index == 0)
        {
            //do nothing
        }
        else if (index == 1)
        {
            wheelList[4].SetSiblingIndex(0); Droite();
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.name);
            

        }
        else if (index == -1)
        {
            wheelList[0].SetSiblingIndex(4); Gauche();
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.name);
        }
        else
        {
            Debug.LogError(index + " is not supported");
        }
    }

    private void IsThereASave(string path)
    {
        if (FileHandler.IsAFileExist(path))
        {
            Debug.Log("File found");
            wheelList[0].SetSiblingIndex(4);
            AffParam("Load");
            wheelList.Clear();
            for (int i = 0; i < wheel.childCount; i++)
            {
                wheelList.Add(wheel.GetChild(i));
            }

        }

    }

    public void Play()
    {
        SceneLoader.Instance.LoadNewScene("Office");
        MasterManager.Instance.currentPhase = Phases.Phase_1;
        StartGame.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credit()
    {
        Debug.Log("Credit code here");
    }

    private void EventCall()
    {
        //ScenarioManager.Instance.LoadScenario(); Bug
    }

    private void Gauche()
    {
        for (int i = 0; i < wheelList.Count; i++)
        {
            if (i!=0)
            {
                LeanTween.move(wheelList[i].gameObject, pos[i - 1],animSpeed);
            }
            else if (i==0)
            {
                LeanTween.move(wheelList[i].gameObject, pos[4], animSpeed);
            }
        }
    }

    private void Droite()
    {
        for (int i = 0; i < wheelList.Count; i++)
        {
            if (i != 4)
            {
                LeanTween.move(wheelList[i].gameObject, pos[i + 1], animSpeed);
            }
            else if (i == 4)
            {
                LeanTween.move(wheelList[i].gameObject, pos[0], animSpeed);
            }
        }
    }



    /*private void ChangeQualityText()
    {
        qualityButton.GetComponentInChildren<Text>().text = "Graphics: " + qualityChosen;
    }

    public void ChangeQualityButton()
    {
        if (qualityChosen == Quality.Low)
        {
            qualityChosen = Quality.Medium;
            ChangeQualityText();
        }
        else if (qualityChosen == Quality.Medium)
        {
            qualityChosen = Quality.High;
            ChangeQualityText();
        }
        else if (qualityChosen == Quality.High)
        {
            qualityChosen = Quality.Low;
            ChangeQualityText();
        }
    }*/
}
