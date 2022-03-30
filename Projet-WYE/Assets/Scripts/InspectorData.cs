using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InspectorData : MonoBehaviour
{
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;

    [SerializeField] private bool testBool = false;
    [SerializeField] private bool hasGenerate = false;
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
        inspection.CreateNewText(_dataList);
    }

    public void DeSelected()
    {
        inspection.ClearAllText();
    }
}
