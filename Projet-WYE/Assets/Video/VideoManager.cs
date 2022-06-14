using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip introCinematic;
    public VideoClip videoClip;
    [SerializeField] private VideoPlayer videoPlayer;
    public string sceneToLoad;
    
    private void Start() {
        if(!MasterManager.Instance.hasSeenIntro && MasterManager.Instance.displayIntro){
            videoPlayer.clip = introCinematic;
            sceneToLoad = "TrappedMan";
            MasterManager.Instance.hasSeenIntro = true;
            StartCoroutine(WaitForVideoEnd());
        }
        else{
            videoPlayer.clip = videoClip;
            sceneToLoad = "TutoScene";

            StartCoroutine(WaitForVideoEnd());
        }
    }

    IEnumerator WaitForVideoEnd(){
        float _delay = ToSingle(videoPlayer.clip.length);
        yield return new WaitForSeconds(_delay);
        SceneLoader.Instance.LoadNewScene(sceneToLoad);
    }
    
    private static float ToSingle(double value){
        return (float)value;
    }
}
