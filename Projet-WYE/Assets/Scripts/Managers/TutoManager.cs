using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoManager : Singleton<TutoManager>
{
    [SerializeField] private int progression = 0;



    public TMP_Text grabText;
    public GameObject canvas;
    public TMP_Text pointAndClickText;

    public bool updateTutoriel;

    private void Awake()
    {
        Projection.Instance.transitionValue = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (FileHandler.IsAFileExist("SaveLastCall.json"))
        {
            // SkipTuto(); //To Delete after testing
            Debug.Log("File Found");
        }
        
        //UpdateText(1);
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
                Debug.Log("1");
                InitTutorial.Instance.grab.SetActive(true);
                break;
            case 2:
                InitTutorial.Instance.grabText.text = "Relâchez la gachette pour lâcher l'objet";
                UpdateIndication(3);
                break;
            case 3:
                InitTutorial.Instance.grabText.text = "Bravo !";
                Progress(4);
                break;

            case 4:
                UpdateText(3);
                canvas.SetActive(true);
                break;

            case 5:
                InitTutorial.Instance.pointAndClick.SetActive(true);
                break;

            case 6:
                UpdateIndication(1);
                InitTutorial.Instance.pointAndClickText.text = "Appuyer sur [A] pour selectionner l'objet";
                break;

            case 7:
                InitTutorial.Instance.pointAndClickText.text = "Bravo !";
                break;

            case 8:
                InitTutorial.Instance.pointAndClickText.text = "En selectionnant deux objets, vous créer une combinaison combinaisons";
                break;

            case 9:
                InitTutorial.Instance.orderText.text = "Chaque combinaison, vous donne un ordre. Attrapez le et validez le";
                WordManager.Instance.pullOrders = true;
                WordManager.Instance.PullWord();
                break;

            case 10:
                InitTutorial.Instance.orderText.text = "Super ! Vous avez toutes les cartes en main pour aider au mieux ceux qui vous appelerons";
                WordManager.Instance.pullOrders = true;
                WordManager.Instance.PullWord();
                break;
        }
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
        }
        else if (i == 2)
        {
            UpdateIndication(4);
            grabText.text = "Validez-moi !";
        }
        else if (i == 3)
        {
            UpdateIndication(3);
            pointAndClickText.text = "Attrapez-moi !";
        }
        else if (i == 4)
        {
            UpdateIndication(4);
            pointAndClickText.text = "Validez-moi pour imaginez des objets !";
        }
    }


    public void Skip()
    {
        SceneLoader.Instance.LoadNewScene("Menu");
    }
}
