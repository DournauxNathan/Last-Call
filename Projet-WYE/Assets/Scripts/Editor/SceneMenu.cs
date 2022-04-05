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
    public static void OpenOffice()
    {
        //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "Assets/Scenes/Office.unity");
        OpenScene("Office");
    }

    [MenuItem("Scenes/Calls/#1 Trapped Man")]
    public static void OpenCall1()
    {
        //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "Assets/Scenes/Gameplay_Combination_Iteration.unity");
        OpenScene("Gameplay_Combination_Iteration");
    }

    [MenuItem("Scenes/Calls/#2 Home Invasion")]
    public static void OpenCall2()
    {
        //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "Assets/Scenes/HomeInvasion.unity");
        OpenScene("HomeInvasion");
    }

    [MenuItem("Scenes/Calls/#3 Rising Water")]
    public static void OpenCall3()
    {
        //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "Assets/Scenes/RisingWater.unity");
        OpenScene("RisingWater");
    }

    [MenuItem("Scenes/Add Persistent")]
    public static void AddPersistent()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Additive);
    }

    private static void OpenScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}