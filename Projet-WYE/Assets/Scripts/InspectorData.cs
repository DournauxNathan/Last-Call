using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorData : MonoBehaviour
{
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;

    void Start()
    {
        inspection = InspectionInWorld.Instance;
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
