using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new  UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
    public ScreenFader screenFader = null;

    private bool isLoading = false;

    public bool startTransition;
    public string nameScene;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
    }

    private void Update()
    {
        if (startTransition)
        {
            LoadNewScene(name);
        }
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

        OnLoadBegin?.Invoke(); // or OnLoadBegin.Invoke(); ? => See if its null of not
        //yield return screenFader.StartFadeIn();

        //yield return StartCoroutine(UnloadCurrent());

        //For Testing
       yield return new WaitForSeconds(3.0f);

        yield return StartCoroutine(LoadNew(sceneName));
        yield return screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();
        startTransition = false;
        isLoading = false;
    }

    private IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

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
    }

    //Debug & Test
    public void LoadGame()
    {
        startTransition = true;
    }

    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }
}