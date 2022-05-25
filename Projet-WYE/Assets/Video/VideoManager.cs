using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip videoClip;
    
    private void Start() {
        StartCoroutine(WaitForVideoEnd());
    }

    IEnumerator WaitForVideoEnd(){
        float _delay = ToSingle(videoClip.length);
        yield return new WaitForSeconds(_delay);
        SceneLoader.Instance.LoadNewScene("TutoScene");
    }
    
    private static float ToSingle(double value){
        return (float)value;
    }
}
