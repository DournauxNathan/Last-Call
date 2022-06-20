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
    [SerializeField] private SherlockEffect sherlockEffect;
    
    private void Start() {
        if(!MasterManager.Instance.hasSeenIntro && MasterManager.Instance.displayIntro)
        {
            DisableTracking();
            videoPlayer.clip = introCinematic;
            sceneToLoad = "Office";
            MasterManager.Instance.hasSeenIntro = true;
            StartCoroutine(WaitForVideoEnd());
        }
        else if (MasterManager.Instance.skipIntro)
        {
            StartCoroutine(WaitForVideoEnd());
        }
        else
        {
            videoPlayer.clip = videoClip;
            sceneToLoad = "TutoScene";

            StartCoroutine(WaitForVideoEnd());
        }
    }

    IEnumerator WaitForVideoEnd(){
        float _delay = ToSingle(videoPlayer.clip.length);
        yield return new WaitForSeconds(_delay);
        Projection.Instance.SetTransitionValue(0);

        if (sceneToLoad == "Office")
        {
            MasterManager.Instance.ChangeSceneByName(1, "Office");
        }
        else if (MasterManager.Instance.isTutoEnded)
        {
            MasterManager.Instance.ChangeSceneByName(0, "Menu");
        }
        else
        {
            SceneLoader.Instance.LoadNewScene(sceneToLoad);
        }
    }
    
    private static float ToSingle(double value){
        return (float)value;
    }

    private void DisableTracking(){
        GameObject _player = sherlockEffect.gameObject;
        Destroy(sherlockEffect);
        _player.transform.position = new Vector3(0, 1, 100); 
    }
}
