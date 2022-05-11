using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectorData : MonoBehaviour
{
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;
    public Image spriteSheet;

    [SerializeField] private bool testBool = false;
    [SerializeField] private bool hasGenerate = false;

    [SerializeField] private bool security = false;
    void Start()
    {
        inspection = InspectionInWorld.Instance;
    }

    private void Update()
    {
        if (testBool && !hasGenerate)
        {
            testBool = false; hasGenerate = true;
            InSelected();
        }
        else if (testBool && hasGenerate)
        {
            testBool = false; hasGenerate = !true;
            DeSelected();
        }
    }

    public void InSelected()
    {
        if (!security)
        {
            inspection.CreateNewText(_dataList);
            security = true;
        }
    }

    public void DeSelected()
    {
        if (security)
        {
            inspection.ClearAllText();
        }
    }
}
