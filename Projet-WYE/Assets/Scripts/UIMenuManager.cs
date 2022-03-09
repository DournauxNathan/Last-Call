using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMenuManager : MonoBehaviour
{
   [SerializeField] private bool firstSetUp = false;

    //MainParam
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform wheel; [SerializeField] private Transform wheelParameters;
    [SerializeField] private List<Transform> wheelList; [SerializeField] private List<Transform> wheelParmetersList;
    [Space(20)] [SerializeField] private Transform currentSelected;
    [SerializeField] private Transform oldSelected;

    /*//Quality Param
    public enum Quality
    {
        Low,Medium,High
    }
    [Space(15)]
    [SerializeField] private Transform qualityButton;
    public Quality qualityChosen = Quality.Medium;*/

    // Start is called before the first frame update
    void Start()
    {
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
        SetUp();
        EventSystem.current.SetSelectedGameObject(wheelList[2].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSelected != EventSystem.current.currentSelectedGameObject.gameObject.transform)
        {
            oldSelected = currentSelected;
            OnWheelUpdate();
        }
        CurrentSelected();
    }

    private void SetUp() 
    {
        wheelList.Clear();
        for (int i = 0; i < wheel.childCount; i++)
        {
            wheelList.Add(wheel.GetChild(i));
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
        currentSelected = EventSystem.current.currentSelectedGameObject.transform;
    }

    private int DirectionWheel()
    {

        if (EventSystem.current.currentSelectedGameObject.gameObject.transform == wheelList[1])
        {
            return 1;
        }
        else if(EventSystem.current.currentSelectedGameObject.gameObject.transform == wheelList[3])
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
            wheelList[4].SetSiblingIndex(0);
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.gameObject.name);
            

        }
        else if (index == -1)
        {
            wheelList[0].SetSiblingIndex(4);
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.gameObject.name);
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
            EventSystem.current.SetSelectedGameObject(wheelList[3].gameObject);
            UpdateWheel(-1);
            CurrentSelected();
            AffParam("Load");
           
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
