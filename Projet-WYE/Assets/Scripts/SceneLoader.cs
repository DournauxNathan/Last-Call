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

#if UNITY_EDITOR
        ScenarioManager.Instance.SetCurrentScenario(ScenarioManager.Instance.currentIndexScenario);
        ScenarioManager.Instance.LoadScenario();
        this.CallWithDelay(LoadNewSceneEditorOnly, .2f);
#endif
    }

    public void LoadNewSceneEditorOnly()
    {
        if (nameScene== "TrappedMan" || nameScene == "HomeInvasion" || nameScene == "RisingWater")
        {
            MasterManager.Instance.currentPhase = Phases.Phase_2;
            MasterManager.Instance.isInImaginary = true;
        }
        else if (nameScene == "Office")
        {
            MasterManager.Instance.currentPhase = Phases.Phase_1;
            MasterManager.Instance.isInImaginary = false;
        }
        else
        {
            MasterManager.Instance.currentPhase = Phases.Phase_0;
            MasterManager.Instance.isInImaginary = false;
        }
        
        MasterManager.Instance.ChangeSceneByName((int)MasterManager.Instance.currentPhase, nameScene);

        //LoadNewScene(nameScene);
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

    public void AddNewScene(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(AddScene(sceneName));
            Projection.Instance.isTransition = false;
        }
    }

    public void Unload(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(UnloadScene(sceneName));
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;

        OnLoadBegin?.Invoke();
        //yield return screenFader.StartFadeIn();

        //Debug.Log(currentScene.name != string.Empty && currentScene.name != "Persistent");

        if (SceneManager.sceneCount >= 2)
        {            
            yield return StartCoroutine(UnloadCurrent());
        }

        if (sceneName == "Appartment_Day1" )
        {
            HeadPhoneManager.Instance.headPhone.DisableHeadset();
        }

        yield return StartCoroutine(LoadNew(sceneName));
        Projection.Instance.isTransition = false;
        //  yield return screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();

        isLoading = false;

        //Debug.Log(sceneName);
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

    private IEnumerator AddScene(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator UnloadScene(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(sceneName);

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
         return SceneManager.GetSceneAt(1);
    }

    public void MoveGO(GameObject go)
    {
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneAt(0));
    }

}