using UnityEngine;

public class OnClick : MonoBehaviour
{
    //public UnityEvent onClick;
    public string s;

    public void Click()
    {
        //onClick?.Invoke();
        MasterManager.Instance.ChangeSceneByName(1,s);
    }

   
}
