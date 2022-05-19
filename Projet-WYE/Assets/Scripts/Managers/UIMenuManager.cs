using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class UIMenuManager : MonoBehaviour
{
   [SerializeField] private bool firstSetUp = false;

    //MainParam
    [Header("Param")]
    [Range(0.1f,1f)]public float animSpeed = 0.3f;
    [Range(0.1f,1f)]public float newSize = 0.3f;

    public List<AudioClip> uiSounds;

    [Header("Debug")]
    [HideInInspector][SerializeField] private Transform mainMenu;
    [HideInInspector] [SerializeField] private Transform wheel;
    [HideInInspector] [SerializeField] private Transform wheelParameters;
    [SerializeField]private List<Transform> wheelList;
    [SerializeField] private List<Transform> wheelParmetersList;
    [SerializeField] private Transform currentSelected;
    [SerializeField] private Transform oldSelected;
    [SerializeField] private List<Vector3> pos;
    [SerializeField] private List<Vector3> posF;
    [SerializeField] private bool readyToDeletG = false;
    [SerializeField] private bool readyToDeletD = false;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private float baseSize;
    private Button settingButton;
    [SerializeField]private Sprite baseOptionImage;
    [SerializeField]private Sprite selectedOptionImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform sliderParent;
    [SerializeField] private List<MySlider> sliders;

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
        audioSource = GetComponent<AudioSource>();
        //Base Size when the list is created
        baseSize = 1;
        //EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        wheelList = new List<Transform>();
        SetUp();
        EventSystem.current.SetSelectedGameObject(wheelList[2].GetChild(0).gameObject);

        StartGame.AddListener(EventCall);
        eventSystem = EventSystem.current;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (LeanTween.isTweening(wheelList[0].gameObject))
        {
            MasterManager.Instance.references.eventSystem.GetComponent<BaseInputModule>().enabled = false;
        }
        else if(!LeanTween.isTweening(wheelList[0].gameObject)) //utile ?
        {
            MasterManager.Instance.references.eventSystem.GetComponent<BaseInputModule>().enabled = true;

        }   

        if (EventSystem.current.currentSelectedGameObject != null && currentSelected != EventSystem.current.currentSelectedGameObject.gameObject.transform && !LeanTween.isTweening(wheelList[0].gameObject))
        {
            CurrentSelected();
            oldSelected = currentSelected;
            OnWheelUpdate();


        }

        //avoid error
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(wheelList[2].GetChild(0).gameObject);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Slider")
        {
            if (settingButton == null)
            {
                foreach (var parent in wheelList)
                {
                    if (parent.GetChild(0).name == "Options")
                    {
                        settingButton = parent.GetChild(0).GetComponent<Button>();
                        //baseOptionImage = settingButton.image;
                        //selectedOptionImage = settingButton.spriteState.highlightedSprite;
                    }
                }

                settingButton.image.sprite = selectedOptionImage;
            }
        }
        else if (settingButton != null)
        {
            settingButton.image.sprite = baseOptionImage;
            settingButton = null;
        }

        if (wheelList.Count !=5 && readyToDeletG && !LeanTween.isTweening(wheelList[5].gameObject))
        {
            readyToDeletG = false; //Debug.Log("Gauche Delet");
            Destroy(wheelList[4].gameObject);
            wheelList.RemoveAt(4);
        }

        if (wheelList.Count != 5 && readyToDeletD && !LeanTween.isTweening(wheelList[5].gameObject))
        {
            readyToDeletD = false; //Debug.Log("Droite Delet");
            Destroy(wheelList[5].gameObject);
            wheelList.RemoveAt(5);
        }

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
            for (int i = 0; i < wheel.childCount; i++)
            {
                pos.Add(wheel.GetChild(i).position); //Debug.Log(wheel.GetChild(i).name);
            }
            CalculFPos();

            sliderParent = wheelParameters.GetChild(3).GetChild(1);
            for (int i = 0; i < sliderParent.childCount; i++)
            {
                sliders.Add(sliderParent.GetChild(i).GetChild(0).GetComponent<MySlider>());
            }
            DisableSlider();

            IsThereASave("SaveLastCall.json");

            //qualityButton.GetComponentInChildren<Text>().text = "Graphics: " + qualityChosen;
        }
    }

    private void OnWheelUpdate()
    {
        UpdateWheel(DirectionWheel());
        DisableSlider();
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
            Droite();
            wheelList[5].SetSiblingIndex(0);  
            SetUp();
            AffParam(EventSystem.current.currentSelectedGameObject.name);
            

        }
        else if (index == -1)
        {
            wheelList[0].SetSiblingIndex(4); 
            Gauche();
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
            Gauche();
            Grandir(wheelList[3]);
            wheelList.Clear();
            for (int i = 0; i < wheel.childCount; i++)
            {
                wheelList.Add(wheel.GetChild(i));
            }

        }
        else
        {
            Grandir(wheelList[2]);
        }
    }

    public void Play()
    {
        MasterManager.Instance.ChangeSceneByName(0, "Office");
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
        WhooshGauche();
        ScaleDown(wheelList);
        Grandir(wheelList[3]);
        for (int i = 0; i < wheelList.Count; i++)
        {
            if (i!=0)
            {
                LeanTween.move(wheelList[i].gameObject, pos[i - 1],animSpeed);
            }
            else if (i==0)
            {
                readyToDeletG = true;
                wheelList.Add(Instantiate(wheelList[i].gameObject, wheel).transform);
                wheelList[5].position = posF[1];
                LeanTween.move(wheelList[i].gameObject, posF[0], animSpeed);
                LeanTween.move(wheelList[5].gameObject, pos[4], animSpeed);
            }
        }
    }

    private void Droite()
    {
        WhooshDroite();
        ScaleDown(wheelList);
        Grandir(wheelList[1]);
        for (int i = 0; i < wheelList.Count; i++)
        {
            
            if (i != 4 && i !=5)
            {
                LeanTween.move(wheelList[i].gameObject, pos[i + 1], animSpeed);
            }
            else if (i == 4)
            {
                readyToDeletD = true;
                wheelList.Add(Instantiate(wheelList[i].gameObject, wheel).transform); //5
                wheelList[5].position = posF[0];
                LeanTween.move(wheelList[i].gameObject, posF[1], animSpeed);
                LeanTween.move(wheelList[5].gameObject, pos[0], animSpeed);
            }
        }
    }

    private void CalculFPos()
    {
        posF = new List<Vector3>();

        float ecartType = pos[1].x - pos[0].x;

        posF.Add(new Vector3(pos[0].x - ecartType, pos[0].y, pos[0].z));
        posF.Add (new Vector3(pos[4].x + ecartType, pos[0].y, pos[0].z));

    }

    private void Grandir(Transform target)
    {
        var _gTarget = target.gameObject;
        var calculatedSize = baseSize + newSize;
        
        LeanTween.scale(_gTarget, new Vector2(calculatedSize, calculatedSize), animSpeed);
    }

    private void ScaleDown(List<Transform> transforms)
    {
        foreach (var transform in transforms)
        {
            LeanTween.scale(transform.gameObject, new Vector2(baseSize, baseSize), animSpeed);
        }
    }

    public void ButtonPressSound()
    {
        audioSource.PlayNewClipOnce(uiSounds[0]);
    }

    public void WhooshDroite()
    {
        audioSource.PlayNewClipOnce(uiSounds[1]);
    }
    public void WhooshGauche()
    {
        audioSource.PlayNewClipOnce(uiSounds[2]);
    }

    public void QuitGameSound()
    {
        audioSource.PlayNewClipOnce(uiSounds[3]);
    }

    private void DisableSlider(){
        foreach (var slider in sliders)
        {
            slider.interactable = false;
        }
    }

    public void EnableSlider(){
        foreach (var slider in sliders)
        {
            slider.interactable = true;
        }
        eventSystem.SetSelectedGameObject(sliders[0].gameObject); //set the first slider as selected
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
