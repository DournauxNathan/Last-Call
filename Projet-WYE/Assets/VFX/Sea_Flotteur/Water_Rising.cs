using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Rising : MonoBehaviour
{
    public GameObject water;
    public float time;
    public float maxheight;
    private bool hasStarted;

    public void StartRising()
    {
        StartCoroutine(Rise());
    }

    private void Update() 
    {
        if(MasterManager.Instance.envIsReveal && !hasStarted)
        {
            hasStarted = true;
            water.SetActive(true);
        }
    }

    public void StopRising()
    {
        StopAllCoroutines();
    }

    public IEnumerator Rise()
    {
        while (water.transform.position.y != maxheight)
        {
            water.transform.position = new Vector3(water.transform.position.x, Mathf.Lerp(water.transform.position.y, maxheight, time * Time.deltaTime), water.transform.position.z);
            yield return null;
        }
    }
}
