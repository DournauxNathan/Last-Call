using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SilhouetteCanvasBehavior : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _defaultTransform;
    public float timeLerping = 2f;
    [SerializeField]private SilhouetteData _silhouetteDataLink;
    
    void Start()
    {
        //_cameraTransform = null; //Remove comment to use camera on the Rig
        if(_cameraTransform == null && MasterManager.Instance != null){ 
            _cameraTransform = MasterManager.Instance.references.mainCamera.transform;
        }
        else if(_cameraTransform == null && MasterManager.Instance == null){
            Debug.LogError("No XRig found to track");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_cameraTransform != null) transform.rotation = _cameraTransform.rotation; //same as camera
    }

    

    public void CheckPositionInList(){
        SilhouetteCanvas _s  = GetComponentInParent<SilhouetteCanvas>();
        if(_s != null){
            foreach(GameObject s in _s._silhouetteCanvasList){
                if(s == this.gameObject){
                    if(_s._silhouetteCanvasList.IndexOf(s)>=_s.canvasPositions.Length){
                        Debug.LogError("Index out of range for: "+s.name+"\n Contain: "+_s._silhouetteCanvasList[_s._silhouetteCanvasList.IndexOf(s)].GetComponentInChildren<TMP_Text>().text);
                        _s._silhouetteCanvasList.Remove(s);
                        Destroy(s);
                        return;
                    }
                    transform.localPosition = _s.canvasPositions[_s._silhouetteCanvasList.IndexOf(s)].FromVector2(0f);
                    _defaultTransform = transform;
                    _silhouetteDataLink = _s.silhouetteDataLink;
                }
            }
        }
        else{
            Debug.LogError("No SilhouetteCanvas found");
        }
    }

    public void OnSelectEnter(){
        //Stop Couroutine that destroy every items
        _silhouetteDataLink.OnHoverEnter();
    }

    public void OnSelectExit(){
        if(_defaultTransform != null){
            transform.localPosition = Vector3.Lerp(transform.position,_defaultTransform.localPosition,timeLerping*Time.deltaTime);
            //start couroutine to destroy every items
            _silhouetteDataLink.OnCancel();
        }
        else{
            Debug.LogError("No default transform found");
        }
    }

}
