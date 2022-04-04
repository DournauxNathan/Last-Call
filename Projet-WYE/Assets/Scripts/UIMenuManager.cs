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
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform wheel; [SerializeField] private Transform wheelParameters;
    [SerializeField] private List<Transform> wheelList; [SerializeField] private List<Transform> wheelParmetersList;
    [Space(20)] [SerializeField] private Transform currentSelected;
    [SerializeField] private Transform oldSelected;



    private List<Animator> animators;
    [SerializeField]private bool isMoving = false;

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
        animators = new List<Animator>();


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

        if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("Gauche") && !isMoving)
        {
            isMoving = true;
            Debug.Log("gauche"); 
            wheelList[4].SetSiblingIndex(0);
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.name);
        }

        if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("Droite") && !isMoving)
        {
            isMoving = true;
            Debug.Log("droite");
            wheelList[0].SetSiblingIndex(4);
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.name);
        }

    }

    private void SetUp() 
    {
        wheelList.Clear();
        animators.Clear();
        for (int i = 0; i < wheel.childCount; i++)
        {
            wheelList.Add(wheel.GetChild(i));
            animators.Add(wheel.GetChild(i).GetComponentInChildren<Animator>());
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
        AllAnimator("Selected", false);
        isMoving = false;

        if (index == 0)
        {
            //do nothing
        }
        else if (index == 1)
        {
            animators[1].SetBool("Selected", true);
            AllAnimator("Gauche");
            

        }
        else if (index == -1)
        {
            animators[3].SetBool("Selected", true);
            AllAnimator("Droite");
            
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
            //EventSystem.current.SetSelectedGameObject(wheelList[2].gameObject);
            //UpdateWheel(-1);
            //CurrentSelected();
            AffParam("Load");
            wheelList.Clear();
            animators.Clear();
            for (int i = 0; i < wheel.childCount; i++)
            {
                wheelList.Add(wheel.GetChild(i));
                animators.Add(wheel.GetChild(i).GetComponentInChildren<Animator>());
            }

        }
        Debug.Log(animators[2].ToString());
        animators[2].SetTrigger("DefaultSelected");
        animators[2].SetBool("Selected", true);

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

    private void AllAnimator(string name)
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger(name);
        }
    }

    private void AllAnimator(string name, bool boolean)
    {
        foreach (var animator in animators)
        {
            animator.SetBool(name, boolean);
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
