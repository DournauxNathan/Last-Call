using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(SphereCollider))]
public class SilhouetteCanvas : MonoBehaviour
{
    public SilhouetteCanvasBehavior _silhouetteCanvasPrefab;
    public List<GameObject> _silhouetteCanvasList = new List<GameObject>();
    public Vector2[] canvasPositions;

    private void Start() {
        if(canvasPositions.Length ==0) canvasPositions = new Vector2[]{new Vector2(1,1),new Vector2(1,-1),new Vector2(-1,1),new Vector2(-1,-1)}; //default if not custom
    }
    public void CreateNewCanvas(List<Outcome> outcomeList){
        foreach(Outcome o in outcomeList){
            SilhouetteCanvasBehavior newCanvas = Instantiate(_silhouetteCanvasPrefab, transform);
            SetValues(newCanvas, o._outcomeText);
            _silhouetteCanvasList.Add(newCanvas.gameObject);
            newCanvas.SetPosition(GetComponent<SphereCollider>());
            //newCanvas.CheckPositionInList();
            newCanvas.OnValidateEvent.AddListener(delegate{o.OnValidate();});

        }   
        /*silhouetteDataLink = data;
        foreach(string s in data.identity.outcomes){
           SilhouetteCanvasBehavior newCanvas = Instantiate(_silhouetteCanvasPrefab, transform);
           SetValues(newCanvas, s);
           _silhouetteCanvasList.Add(newCanvas.gameObject);
           newCanvas.CheckPositionInList();
        }*/
        
    }

    public void OnTP(){
        foreach(GameObject canvas in _silhouetteCanvasList)
        {
            canvas.GetComponent<SilhouetteCanvasBehavior>().UpdateDefaultTransform(GetComponent<SphereCollider>());
        }
    }

    private void SetValues(SilhouetteCanvasBehavior canvas, string text){
        /*canvas.transform.SetParent(this.transform); // useless ?
        canvas.transform.localScale = Vector3.one;
        canvas.transform.localPosition = Vector3.zero;
        canvas.transform.localRotation = Quaternion.identity;*/
        canvas._text.text = text;
    }

    public void DestroyCanvas(){
        foreach(GameObject g in _silhouetteCanvasList){
            Destroy(g);
        }
        _silhouetteCanvasList.Clear(); //clear list to be able to create new canvas
    }
}
