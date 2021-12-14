using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstantiableButton : MonoBehaviour
{
    [Header("Refs")]
    public Button button;
    public Image img;
    public TMP_Text text;

    [HideInInspector] public bool isInstiantiated;

    public int currentClick;
    private QuestionFormat question;
    private OrderFormat order;
    private ObjectActivator swapImaginaire;
    private Transform stock;
    private bool isActive;

    private void Start()
    {
        swapImaginaire = MasterManager.Instance.objectActivator;
    }

    public void Activate(Transform parent, Transform stock, QuestionFormat question, OrderFormat order)
    {
        transform.SetParent(parent);
        
        this.stock = stock;
        
        currentClick = 0;
        button.enabled = true;
        button.interactable = true;
        img.enabled = true;
        isActive = true;

        isInstiantiated = true;

        if (question != null)
        {
            this.question = question;

            UpdateQuestion();
        }
        else if(question != null)
        {
            this.order = order;

            UpdateOrder();
        }
    }

    public void Desactivate()
    {
        isActive = false;
        this.gameObject.SetActive(false);
    }

    public void IncreaseClick()
    {
            if (currentClick < question.listQuestion.Length - 1)
            {
                //Active unitée
                //Debug.Log(button.Value.units[button.Value.currentClick]);
                UnitManager.Instance.AddToUnlock(question.units[currentClick]);


                for (int i = 0; i < question.listIdObject.Length; i++)
                {
                    if (currentClick == question.listIdObject[i].y && question.listIdObject[i].x != 0)
                    {
                        swapImaginaire.indexesList.Add(Mathf.FloorToInt(question.listIdObject[i].x));
                    }
                }
                currentClick++;
            }
            else if (currentClick >= question.listQuestion.Length - 1)
            {
                //Active unitée, boucle infinit quand click
                //Debug.Log(button.Value.units[button.Value.currentClick]);
                UnitManager.Instance.AddToUnlock(question.units[currentClick]);

                for (int i = 0; i < question.listIdObject.Length; i++)
                {
                    if (currentClick == question.listIdObject[i].y && !swapImaginaire.indexesList.Contains(question.listIdObject[i].x) && question.listIdObject[i].x != 0)
                    {
                        //Debug.Log(button.Value.listIdObject[i].x);
                        swapImaginaire.indexesList.Add(Mathf.FloorToInt(question.listIdObject[i].x));
                    }
                }

                Desactivate();
            }
        UpdateQuestion();
    }

    private void ReputOnStock()
    {
        button.enabled = false;
        img.enabled = false;

        Desactivate();
        transform.SetParent(stock);
        isInstiantiated = false;
    }
    private void UpdateQuestion()
    {
        if(isActive)
        {
            text.text = question.listQuestion[currentClick];
        }
        else
        {
            text.text = string.Empty;
            button.interactable = false;
        }
    }

    private void UpdateOrder()
    {
        if (isActive)
        {
            text.text = order.orderText;
        }
        else
        {
            text.text = string.Empty;
            button.interactable = false;
        }
    }
}
