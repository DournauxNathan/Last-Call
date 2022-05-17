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

    [MenuItem("Scenes/Tutoriel")]
    public static void AddTuto()
    {
        OpenScene("TutoScene");
    }

    #region Appartments
    [MenuItem("Scenes/Apparts/2")]
    public static void AddAppart2()
    {
        AddAppartement(2);
    }

    [MenuItem("Scenes/Apparts/1")]
    public static void AddAppart1()
    {
        AddAppartement(1);
    }


    [MenuItem("Scenes/Apparts/0")]
    public static void AddAppart0()
    {
        AddAppartement(0);
    }

    [MenuItem("Scenes/Apparts/-1")]
    public static void AddAppart_1()
    {
        AddAppartement(-1);
    }
    [MenuItem("Scenes/Apparts/-2")]
    public static void AddAppart_2()
    {
        AddAppartement(-2);
    }
    #endregion

    public static void AddAppartement(int i)
    {
        switch (i)
        {
            case 2:
                OpenAppart("Appartment_Day+2");
                break;
            case 1:
                OpenAppart("Appartment_Day+1");
                break;
            case 0:
                OpenAppart("Appartment_Day_0");
                break;
            case -1:
                OpenAppart("Appartment_Day-1");
                break;
            case -2:
                OpenAppart("Appartment_Day-2");
                break;
        }
    }

    private static void OpenScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }

    private static void OpenAppart(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/Appartements/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}