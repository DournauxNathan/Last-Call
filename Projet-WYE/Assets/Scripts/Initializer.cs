using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Initializer : MonoBehaviour
{
    public UnityEvent onStart, onCall;


    public List<GameObject> objs;

    // Start is called before the first frame update
    void Start()
    {
        onStart?.Invoke();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (MasterManager.Instance.envIsReveal)
        {
            onCall?.Invoke();
        }
    }

    public void ToogleObjInList(bool value)
    {
        foreach (GameObject gameObject in objs)
        {
            gameObject.SetActive(value);
        }
    }
}
