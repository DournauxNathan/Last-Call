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
        if (!security && inspection._containers.childCount == 0)
        {
            inspection.CreateNewText(_dataList);
            security = true;
        }
    }

    public void DeSelected()
    {
        if (security && inspection._containers.childCount != 0)
        {
            security = false;
            inspection.ClearAllText();
        }
    }
}
