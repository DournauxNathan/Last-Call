using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SavingLayerAndTag 
{

    [MenuItem("TagsAndLayer/Save")]
    public static void SaveLayer()
    {
        var allObject = GameObject.FindObjectsOfType<CombinableObject_Data>();
        foreach (var itemComp in allObject)
        {
            var item = itemComp.gameObject;
            var component = item.GetComponent<TagAndLayerSaver>();
            if (component == null)
            {
                component = item.AddComponent<TagAndLayerSaver>();
            }
            component.myLayer = item.layer;
            component.myTag = item.tag;
        }
    }

    [MenuItem("TagsAndLayer/Load")]
    public static void LoadLayer()
    {
        var allObject = GameObject.FindObjectsOfType<CombinableObject_Data>();
        foreach (var itemComp in allObject)
        {
            var item = itemComp.gameObject;
            var component = item.GetComponent<TagAndLayerSaver>();
            if (component == null)
            {
                return;
            }
            item.layer = 3;
            item.tag = component.myTag;
        }
    }
}
