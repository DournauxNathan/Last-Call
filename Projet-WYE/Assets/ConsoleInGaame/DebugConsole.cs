using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugConsole : Singleton<DebugConsole>
{
    [SerializeField]
    public GameObject textPrefab;
    [SerializeField]
    private Transform stock;
    [SerializeField]
    private Transform container;

    [SerializeField]
    private Color green;
    [SerializeField]
    private Color red;
    [SerializeField]
    private Color orange;

    private int minButton = 100;

    public TMP_Text text;

    public string output = "";
    public string stack = "";


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < minButton; i++)
        {
            TMP_Text _text;
            _text = Instantiate(textPrefab, stock).GetComponent<TMP_Text>();
            _text.enabled = false;

        }
        /*Debug.LogWarning("test");
        Debug.LogError("coucou");*/
        
    }


    private void OnEnable()
    {
        //Application.logMessageReceived += HandleLog;
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        //Application.logMessageReceived -= HandleLog;
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    public void Log(string textToSend)
    {
        text = stock.GetComponentInChildren<TMP_Text>();
        text.enabled = true;    
        text.text = textToSend; text.color = green;
        text.GetComponent<Transform>().SetParent(container);
        checkMax();
    }

    public void Warning(string textToSend)
    {
        text = stock.GetComponentInChildren<TMP_Text>();
        text.enabled = true;
        text.text = textToSend; text.color = orange;
        text.GetComponent<Transform>().SetParent(container);
        checkMax();
    }

    public void Error(string textToSend)
    {
        text = stock.GetComponentInChildren<TMP_Text>();
        text.enabled = true;
        text.text = textToSend; text.color = red;
        text.GetComponent<Transform>().SetParent(container);
        checkMax();
    }

    [System.Obsolete]
    private void checkMax()
    {
        while (container.GetChildCount()>=12)
        {
            TMP_Text _text = container.GetChild(0).GetComponent<TMP_Text>();
            _text.enabled = false;
            container.GetChild(0).SetParent(stock);
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                Error(logString);
                break;
            case LogType.Assert:
                break;
            case LogType.Warning:
                Warning(logString);
                break;
            case LogType.Log:
                Log(logString);
                break;
            case LogType.Exception:
                break;
        }
        output = logString;
        stack = stackTrace;
    }



}

