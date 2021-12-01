using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new  UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
    [HideInInspector] public string nameScene;
    public ScreenFader screenFader;

    private Scene currentScene;
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
            StartCoroutine(LoadScene(nameScene));
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;

        OnLoadBegin?.Invoke();
        //yield return screenFader.StartFadeIn();

        if (currentScene.name != null && currentScene.name != "Persistent")
        {
            yield return StartCoroutine(UnloadCurrent());
        }

        yield return StartCoroutine(LoadNew(sceneName));

        //yield return screenFader.StartFadeOut();
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
}