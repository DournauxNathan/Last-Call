using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SilhouetteCanvas : MonoBehaviour
{
    public SilhouetteCanvasBehavior _silhouetteCanvasPrefab;
    public List<GameObject> _silhouetteCanvasList = new List<GameObject>();
    public Vector2[] canvasPositions;
    public SilhouetteData silhouetteDataLink;

    private void Start() {
        if(canvasPositions.Length ==0) canvasPositions = new Vector2[]{new Vector2(1,1),new Vector2(1,-1),new Vector2(-1,1),new Vector2(-1,-1)}; //default if not custom
    }
    public void CreateNewCanvas(SilhouetteData data)
    {
        silhouetteDataLink = data;
        foreach(string s in data.outcomes){
           SilhouetteCanvasBehavior newCanvas = Instantiate(_silhouetteCanvasPrefab, transform);
           SetValues(newCanvas.gameObject, s);
           _silhouetteCanvasList.Add(newCanvas.gameObject);
           newCanvas.CheckPositionInList();
        }
    }

    private void SetValues(GameObject canvas, string text){
        /*canvas.transform.SetParent(this.transform); // useless ?
        canvas.transform.localScale = Vector3.one;
        canvas.transform.localPosition = Vector3.zero;
        canvas.transform.localRotation = Quaternion.identity;*/
        TMP_Text _text = canvas.GetComponentInChildren<TMP_Text>();
        _text.text = text;
    }

    public void DestroyCanvas(){
        foreach(GameObject g in _silhouetteCanvasList){
            Destroy(g);
        }
    }
}
