using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoManager : Singleton<TutoManager>
{
    [SerializeField] private int progression = 0;



    public GameObject canvas1;
    public TMP_Text grabText;
    public GameObject canvas2;
    public GameObject canvas3;
    public TMP_Text pointAndClickText;

    public bool updateTutoriel;

    public bool isPointDone;

    public bool firstPartIsDone;
    public bool secondPartIsDone;

    private void Awake()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_0)
        {
            Projection.Instance.transitionValue = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        UpdateText(1);

        if (FileHandler.IsAFileExist("SaveLastCall.json"))
        {
            // SkipTuto(); //To Delete after testing
            Debug.Log("File Found");
        }
        
    }

    private void Update()
    {
        if (updateTutoriel)
        {
            updateTutoriel = false;
            progression++;
            Progress(progression);
        }
    }
    public void ActiveText(GameObject _text)
    {
        _text.SetActive(true);
    }

    public void Progress(int i)
    {
        progression = i;

        if (progression >= i)
        {
            UpdateTutorial();
        }
    }

    public void UpdateTutorial()
    {
        switch (progression)
        {
            case 1:
                InitTutorial.Instance.grab.SetActive(true);
                break;
            case 2:
                InitTutorial.Instance.grabText.text = "Relâchez la gachette pour lâcher l'objet";
                UpdateIndication(3);
                break;
            case 3:
                InitTutorial.Instance.grabText.color = new Color(0, 226, 255);
                InitTutorial.Instance.grabText.text = "Bravo !";
                Progress(4);
                break;

            case 4:
                UpdateText(1);
                canvas2.SetActive(true);
                break;

            case 5:
                InitTutorial.Instance.order.SetActive(true);
                InitTutorial.Instance.pointAndClick.SetActive(true);
                InitTutorial.Instance.orderText.text = "Le jeu propose des puzzles basé sur la combinaison d'objets";
                InitTutorial.Instance.pointAndClickText.text = "Pointer un des objets en bleu en passant le rayon dessus";
                break;

            case 6:
                if (isPointDone)
                {
                    Progress(8);
                }
                else
                {
                    UpdateIndication(1);
                    InitTutorial.Instance.pointAndClickText.text = "Si un contour apparait vous pouvez \n appuyer sur [A] ou [X] pour le selectionner";
                }         
                break;

            case 7:
                if (isPointDone)
                {
                    Progress(9);
                }
                else
                {
                    InitTutorial.Instance.pointAndClickcomplentaire.SetActive(true);
                    InitTutorial.Instance.pointAndClickText.text = "Bravo !";
                    isPointDone = true;
                    Progress(8);
                }
                break;

            case 8:
                InitTutorial.Instance.pointAndClickText.text = "En selectionnant deux objets, vous créer une combinaison";
                break;

            case 9:
                InitTutorial.Instance.orderText.text = "Chaques combinaisons, vous donne un ordre. Attrapez le et validez le";
                
                //WordManager.Instance.pullOrders = true;
                if (isPointDone)
                {
                    WordManager.Instance.PullWord();
                }
                UpdateIndication(2);
                Progress(10);
                break;

            case 10:
                InitTutorial.Instance.orderText.text = "Maintenez [B] ou [Y] pour continuer";
                firstPartIsDone = true;
                Projection.Instance.transitionValue = 50f;
                break;

            case 11:
                Projection.Instance.enableTransition = true;
                InitTutorial.Instance.orderText.text = "Vous êtes à présent dans l'imaginaire de Josh";
                canvas3.SetActive(true);
                break;
        }
    }

    public void IndicateButton(bool value)
    {
        ColorIndicator.Instance.indicateButton = value;
    }

    public void UpdateIndication(int i)
    {
        MasterManager.Instance.buttonEmissive = i;
    }

    public void UpdateText(int i)
    {
        if (i == 1)
        {
            UpdateIndication(3);
            grabText.text = "Attrapez-moi !";
            pointAndClickText.text = "Attrapez-moi !";
        }
        else if (i == 2)
        {
            UpdateIndication(4);
            grabText.text = "Validez-moi !";
            pointAndClickText.text = "Validez-moi !";
        }
    }


    public void Skip()
    {
        SceneLoader.Instance.LoadNewScene("Menu");
    }
}
