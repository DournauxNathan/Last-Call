using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InspectorData : MonoBehaviour
{
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;
    public float delay = 0.1f;
    public bool hasRandom = false;

    [SerializeField] private bool testBool = false;
    [SerializeField] private bool hasGenerate = false;

    [SerializeField] private bool security = false;
    [SerializeField] private InspectorEffect inspectorEffect;
    void Start()
    {
        inspection = InspectionInWorld.Instance;
        inspectorEffect = InspectorEffect.Instance;
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
            inspection.CreateNewText(_dataList,delay,hasRandom);
            inspectorEffect.objectTransform = transform;
            inspectorEffect.transform.position = transform.position;
            security = true;
        }
    }

    public void DeSelected()
    {
        if (security)
        {
            inspection.ClearAllText();
            inspection.StopGenerating();
            inspectorEffect.objectTransform = null;
        }
        security = false;
    }
}
