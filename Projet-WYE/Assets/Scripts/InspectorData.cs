using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorData : MonoBehaviour
{
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;

    // Start is called before the first frame update
    void Start()
    {
        inspection = InspectionInWorld.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
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
