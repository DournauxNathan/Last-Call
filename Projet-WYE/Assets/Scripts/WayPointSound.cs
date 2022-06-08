using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WayPointSound : Singleton<WayPointSound>
{
    [SerializeField] private bool testBool = false;
    public AudioClip waypointSound;
    public List<Transform> waypoints;
    private AudioSource _audioSource;
    private bool _isLerping;
    public float lerpTime;
    public float lerpSpeed;
    private float _lerpTime;
    

    void Start()
    {
        if(TryGetComponent<AudioSource>(out _audioSource))
        {
            _audioSource.spatialBlend = 1;
        }
        else{
            Debug.Log("AudioSource not found"+ this.gameObject);
        }
        Attach();
    }

    void Update()
    {
        if(testBool)
        {
            ChangeLocation();
            testBool = false;
        }
        if(_isLerping)
        {
            Lerping();
        }
    }

    private void Lerping(){
        transform.position = Vector3.Lerp(transform.position, waypoints[0].position, lerpSpeed*Time.deltaTime);
        _lerpTime-=Time.deltaTime;
        if(_lerpTime<=0){
            _isLerping = false;
            _lerpTime = lerpTime;
            waypoints.RemoveAt(0);
            Attach();
        }
    }
    private void Attach(){
        if(waypoints.Count>0){
            transform.SetParent(waypoints[0]);
            transform.position = waypoints[0].position;
        }
        else{
            Debug.Log("No waypoints found"+ this.gameObject);
        }
    }
    private void Detach(){
        transform.DetachChildren();
    }

    public void ChangeLocation(){
        if(!_isLerping && waypoints.Count > 0){
            Detach();
            _audioSource.PlayNewClipOnce(waypointSound);
            _isLerping = true;
        }
    }
}
