using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    [MenuItem("Scenes/Menu")]
    public static void OpenMenu()
    {
        OpenScene("Menu");
    }

    [MenuItem("Scenes/Office")]
    public static void OpenGame()
    {
        OpenScene("Office");
    }

    [MenuItem("Scenes/Calls/Call1")]
    public static void OpenCall1()
    {
        OpenScene("Call1");
    }

    [MenuItem("Scenes/Calls/Call2")]
    public static void OpenCall2()
    {
        OpenScene("Call2");
    }

    [MenuItem("Scenes/Calls/Call3")]
    public static void OpenCall3()
    {
        OpenScene("Call3");
    }

    [MenuItem("Scenes/Gameplay/Iteration#1")]
    public static void Gameplay()
    {
        OpenScene("Gameplay_Combination_Iteration");
    }

    private static void OpenScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}