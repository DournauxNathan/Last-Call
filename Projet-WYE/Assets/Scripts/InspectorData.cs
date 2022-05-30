using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectorData : MonoBehaviour
{
    public int memoLink;
    private InspectionInWorld inspection;
    [Header("Data")]
    public List<string> _dataList;
    public float delay = 0.1f;
    public bool hasRandom = false;
    public Sprite sprite;
    public float spriteOffset;
    public float spriteGlobalScale;

    [SerializeField] private bool testBool = false;
    private bool hasGenerate = false;

    private bool security = false;
    private InspectorEffect inspectorEffect;
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
            SpriteSheetReader.Instance.CallPlaySouvenirs();
        }
        else if (testBool && hasGenerate)
        {
            testBool = false; hasGenerate = !true;
            DeSelected();
            SpriteSheetReader.Instance.DeSelected();
        }
    }

    public void InSelected()
    {
        inspection.CreateNewText(_dataList,delay,hasRandom);
        if(sprite != null) inspection.DisplaySprite(sprite,spriteOffset,spriteGlobalScale);
        inspectorEffect.objectTransform = transform;
        inspectorEffect.transform.position = transform.position;
        SpriteSheetReader.Instance.memoIndex = memoLink;
    }

    public void DeSelected()
    {
        inspection.ClearAllText();
        inspection.StopGenerating();
        inspection.VoidSprite();
        inspectorEffect.objectTransform = null;
    }
}
