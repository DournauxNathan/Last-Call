using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SilhouetteCanvasBehavior : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _defaultTransform;
    public float timeLerping = 2f;
    [SerializeField] private bool testBoolValidate;
    [SerializeField]private bool testBoolExit;
    private bool isLerping = false;
    public TMP_Text _text;
    public UnityEvent OnValidateEvent;
    
    void Start()
    {
        //_cameraTransform = null; //Remove comment to use camera on the Rig
        if(_cameraTransform == null && MasterManager.Instance != null){ 
            _cameraTransform = MasterManager.Instance.references.mainCamera.transform;
        }
        else if(_cameraTransform == null && MasterManager.Instance == null){
            Debug.LogError("No XRig found to track");
        }
        OnValidateEvent.AddListener(DeleteSelfCavas);
    }

    // Update is called once per frame
    void Update()
    {
        if(_cameraTransform != null) transform.rotation = _cameraTransform.rotation; //same as camera

        if(testBoolValidate){
            testBoolValidate = false;
            OnValidateEvent.Invoke();
        }
        if(testBoolExit){
            testBoolExit = false;
            OnSelectExit();
        }
    
        if(transform.position != _defaultTransform && isLerping){
            //Debug.Log("Lerping");
            transform.position = Vector3.Lerp(transform.position, _defaultTransform, timeLerping * Time.deltaTime);
        }
    }

    public void SetPosition(SphereCollider sphere){
        Vector3 _min = sphere.bounds.min;
        Vector3 _max = sphere.bounds.max;
        transform.position = new Vector3(Random.Range(_min.x , _max.x) , Random.Range(_min.y , _max.y), Random.Range(_min.z , _max.z));
        _defaultTransform = transform.position;
        //_s.transform.position = new Vector3((_min.x + _max.x)/2, (_min.y + _max.y)/2, (_min.z + _max.z)/2);
    }

    [System.Obsolete]
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
                    _defaultTransform = transform.position;
                }
            }
        }
        else{
            Debug.LogError("No SilhouetteCanvas found");
        }
    }

    public void OnSelectEnter(){
        //Stop Couroutine that destroy every items
        isLerping =false   ;
    }

    public void OnSelectExit(){
        if(_defaultTransform != null){
            //transform.localPosition = Vector3.Lerp(transform.position,_defaultTransform.localPosition,timeLerping*Time.deltaTime);
            //start couroutine to destroy every items
            isLerping = true;
        }
        else{
            Debug.LogError("No default transform found");
        }
    }

    public void OnValide(){
        OnValidateEvent.Invoke();
        /*SilhouetteManager.Instance.IncreaseCurrentSilhouetteValidation();
        SilhouetteManager.Instance.LastValidation(_silhouetteDataLink.isLastValidation);
        SilhouetteManager.Instance.CheckIfAllValidationAreDone();*/
    }

    private void DeleteSelfCavas(){
        Destroy(this.gameObject);
    }

}
