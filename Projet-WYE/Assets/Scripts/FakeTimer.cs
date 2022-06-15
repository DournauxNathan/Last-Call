using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FakeTimer : MonoBehaviour
{
    [SerializeField]private TMP_Text _text;
    private Transform _transformCamera;
    public float distanceFromCamera;
    public float offset;
    public float speed;
    private int minutes;
    private bool _isActive = false;

    
    void Start()
    {
        if(MasterManager.Instance != null){
            _transformCamera = MasterManager.Instance.references.mainCamera;
        }
        else{
            Debug.LogError("MasterManager is null"+this.gameObject);
        }
        StartCoroutine(StartTimer()); Debug.Log("StartTimer");
    }

    void Update()
    {
        if(_isActive){
            transform.rotation = _transformCamera.rotation;
            transform.position = Vector3.Lerp(transform.position, _transformCamera.position, Time.deltaTime * speed);
        }
    }

    Vector3 CalculedPosition()
    {
        Vector3 resultingPosition = _transformCamera.position + _transformCamera.forward * distanceFromCamera;
        Vector3 calculedOffset = new Vector3(resultingPosition.x, resultingPosition.y + offset, resultingPosition.z);
        return calculedOffset;
    }

    IEnumerator StartTimer()
    {
        while(true){
        yield  return new WaitForSeconds(56f);
        _isActive = true;
        minutes++;
        if(minutes !=1){
            _text.text = minutes + " minutes passées";
        }
        else{
            _text.text = minutes + " minute passée";
        }
        FadeIn();
        yield return new WaitForSeconds(4f);
        FadeOut();
        _isActive = false;
        }
    }

    private void FadeIn(){
        _text.CrossFadeAlpha(1, 1, false);
    }
    private void FadeOut(){
        _text.CrossFadeAlpha(0, 1, false);
    }
}
