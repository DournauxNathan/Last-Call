using UnityEngine;

public class OnClick : MonoBehaviour
{
    //public UnityEvent onClick;
    public string s;

    public void Click()
    {
        //onClick?.Invoke();
        ActivateImaginary();
    }

    public void ActivateImaginary()
    {
        MasterManager.Instance.objectActivator.ActivateObjet();
        SceneLoader.Instance.LoadNewScene(s);
    }

}
