using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new  UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
     public string nameScene;
    public ScreenFader screenFader;

    public Scene currentScene;
    public string cScene;
    private bool isLoading = false;

    private void Start()
    {
        SceneManager.sceneLoaded += SetActiveScene;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadNewScene(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;

        OnLoadBegin?.Invoke();
        //yield return screenFader.StartFadeIn();

        //Debug.Log(currentScene.name != string.Empty && currentScene.name != "Persistent");

        if (currentScene.name != "Null" && currentScene.name != "Persistent")
        {            
            yield return StartCoroutine(UnloadCurrent());
        }

        if (sceneName == "Appartment_Day1" )
        {
            HeadPhoneManager.Instance.headPhone.DisableHeadset();
        }

        yield return StartCoroutine(LoadNew(sceneName));

        //  yield return screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();

        isLoading = false;
    }

    private IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);

        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator LoadNew(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneAt(1);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        //Debug.Log(scene.name);
        currentScene = scene;
    }

    public void LoadScene()
    {
        LoadNewScene(nameScene);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void MoveGO(GameObject go)
    {
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneAt(0));
    }

}