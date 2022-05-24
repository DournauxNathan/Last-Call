using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectorData : MonoBehaviour
{
    public int memoLink;
    private InspectionInWorld inspection;
    private InspectorEffect inspectorEffect;
    [Header("Data")]
    public List<string> _dataList;
    public float delay = 0.1f;
    public bool hasRandom = false;
    public float spriteOffset;
    public float spriteGlobalScale;

    [SerializeField] private bool testBool = false;
    private bool hasGenerate = false;

    private bool security = false;
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

    private void GetInstances(){
        //Debug.Log("GetInstances");
        if (inspection == null)
        {
            inspection = InspectionInWorld.Instance;
            //Debug.Log("inspection: " + inspection);
        }
        if (inspectorEffect == null)
        {
            inspectorEffect = InspectorEffect.Instance;
            //Debug.Log("inspectorEffect: " + inspectorEffect);
        }
    }


    public void InSelected()
    {
        GetInstances();
        inspection.CreateNewText(_dataList,delay,hasRandom);
        inspectorEffect.objectTransform = transform;
        inspectorEffect.transform.position = transform.position;
        SpriteSheetReader.Instance.memoIndex = memoLink;
    }

    public void DeSelected()
    {
        GetInstances();
        inspection.ClearAllText();
        inspection.StopGenerating();
        inspectorEffect.objectTransform = null;
    }
}
