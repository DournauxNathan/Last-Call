using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using TMPro;

public class DialogueViewer : MonoBehaviour
{
    [SerializeField] DialogueController dialoguecontroller;
    [Space(10)]
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] TextMeshProUGUI txtNodeDisplay;
    DialogueController controller;

    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller = dialoguecontroller;
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();


        //Start the dialogue
        var urrentNode = controller.GetCurrentNode();
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        txtNodeDisplay.text = newNode.text;

        KillAllChildren(parentOfResponses);
        for (int i = newNode.responses.Count - 1; i >= 0; i--)
        {
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
            responceButton.GetComponentInChildren<TMP_Text>().text = response.destinationNode;

            if (newNode.tags.Contains("Section"))
            {
                var colors = GetComponent<Button>().colors;
                colors.normalColor = Color.red;
            }

            responceButton.GetComponent<IndexButton>().currentIndex = currentChoiceIndex;
            responceButton.GetComponent<IndexButton>().currentQuestion = response.destinationNode;

            responceButton.GetComponent<IndexButton>().currentResponce = response.displayText;

            responceButton.onClick.AddListener(() => OnNodeSelected(currentChoiceIndex));
        }

        if (newNode.tags.Contains("END"))
        {
            Debug.Log("End of the dialogue");
        }
    }
}