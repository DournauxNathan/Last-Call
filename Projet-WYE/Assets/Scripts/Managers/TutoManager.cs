using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class TutoManager : Singleton<TutoManager>
{
    [SerializeField] private int progression = 0;



    public GameObject canvas1;
    public TMP_Text grabText;
    public GameObject canvas2;
    public GameObject canvas3;
    public TMP_Text pointAndClickText;
    public TMP_Text imaginary;

    public bool updateTutoriel;

    public bool isPointDone;

    public bool firstPartIsDone;
    public bool secondPartIsDone;
    public bool isTutoDone;

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
                InitTutorial.Instance.grabText.text = "Relâchez la gâchette pour lâcher l'objet";
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
                InitTutorial.Instance.orderText.text = "Le jeu propose des puzzles basés sur la combinaison d'objets";
                InitTutorial.Instance.pointAndClickText.text = "Pointez un des objets en bleu en passant le rayon dessus";
                break;

            case 6:
                if (isPointDone)
                {
                    Progress(8);
                }
                else
                {
                    UpdateIndication(1);
                    InitTutorial.Instance.pointAndClickText.text = "Si un contour apparaît vous pouvez \n appuyer sur [A] ou [X] pour le sélectionner";
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
                InitTutorial.Instance.pointAndClickText.text = "En selectionnant deux objets, vous créez une combinaison";
                break;

            case 9:
                InitTutorial.Instance.orderText.text = "Chaque combinaison vous donne un ordre. Attrapez-le et validez-le";

                //WordManager.Instance.pullOrders = true;
                if (isPointDone)
                {
                    UpdateIndication(2);
                    Progress(10);
                    WordManager.Instance.PullWord();
                }
                break;

            case 10:
                InitTutorial.Instance.orderText.text = "";
                UpdateString(InitTutorial.Instance.orderText, "Maintenez [B] ou [Y] pour continuer");
                firstPartIsDone = true;
                Projection.Instance.transitionValue = 50f;
                break;
            case 11:
                Projection.Instance.enableTransition = true;
                break;
            case 12:
                canvas3.SetActive(true);
                canvas1.SetActive(false);
                canvas2.SetActive(false);
                InitTutorial.Instance.pointAndClick.SetActive(false);
                InitTutorial.Instance.grab.SetActive(false);
                InitTutorial.Instance.pointAndClickcomplentaire.SetActive(false);
                InitTutorial.Instance.orderText.text = "";
                UpdateString(InitTutorial.Instance.orderText, "Vous êtes à présent dans l'imaginaire de Josh");
                break;
            case 13:
                InitTutorial.Instance.orderText.text = "";
                this.CallWithDelay(() => UpdateString(InitTutorial.Instance.orderText, "Trouvez un moyen de soigner l'appelant en le combinant avec un objet"), 0f);
                Projection.Instance.enableTransition = false;
                break;
            case 14:
                secondPartIsDone = true;
                InitTutorial.Instance.orderText.text = "";
                UpdateString(InitTutorial.Instance.orderText, "Bravo ! \n Vous serez ammené à combiner différents objets pour \n trouver la meilleure solution aux problèmes rencontrés.");
                Projection.Instance.enableTransition = true;
                Projection.Instance.transitionValue = 50f;
                this.CallWithDelay(ResetString, 17f);
                this.CallWithDelay(() => UpdateString(InitTutorial.Instance.orderText, "Maintenez [B] ou [Y] pour quitter le tutoriel"), 19f);
                Progress(15);
                break;
            case 15:
                isTutoDone = true;
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
            imaginary.text = "Attrapez-moi !";
        }
        else if (i == 2)
        {
            UpdateIndication(4);
            grabText.text = "Validez-moi !";
            pointAndClickText.text = "Validez-moi !";
            imaginary.text = "Validez-moi !";
        }
    }

    public void ResetString()
    {
        InitTutorial.Instance.orderText.text = string.Empty;
    }

    public void UpdateString(TMP_Text text, string s)
    {
        this.CallTypeWriter(text, s);
    }

    public void Destroy()
    {
        Destroy(canvas1);
        Destroy(canvas2);
        Destroy(canvas3);
    }

    public void Skip()
    {
        MasterManager.Instance.Reset();
        SceneLoader.Instance.Unload("TutoScene");
        SceneLoader.Instance.LoadNewScene("Menu");
    }
}
