using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UiPauseManager : Singleton<UiPauseManager>
{
    [SerializeField] private GameObject firstSelectedGO;
    [SerializeField] private Transform MainPause;
    [SerializeField] private List<Transform> SubMenus;
    [Space(10)]
    public UnityEvent OnPauseEnter;
    public UnityEvent OnPauseExit;


    // Start is called before the first frame update
    void Start()
    {
        UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUp()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedGO);
    }

    public void UnPause()
    {
        this.gameObject.SetActive(false);
    }

    public void PauseDisplay()
    {
        this.gameObject.SetActive(true);
        SetUp();
    }

    public void BackToMainMenu()
    {
        MainPause.gameObject.SetActive(true);
        foreach (var sub in SubMenus)
        {
            sub.gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(firstSelectedGO);
    }

    public void DisplayTarget(GameObject target)
    {
        MainPause.gameObject.SetActive(false);
        foreach (var sub in SubMenus)
        {
            sub.gameObject.SetActive(false);
        }
        target.SetActive(true);
    }

    public void SelectTraget(GameObject target)
    {
        EventSystem.current.SetSelectedGameObject(target);
    }

    public void GoBackToMainAppart() {
        //Debug.Log("Go BAck To Main Menu Code Here");
        SceneLoader.Instance.LoadNewScene("Appartment");
    }

}
