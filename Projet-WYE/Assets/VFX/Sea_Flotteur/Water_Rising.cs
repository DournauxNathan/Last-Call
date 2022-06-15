using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Rising : MonoBehaviour
{
    public GameObject water;
    public float time;
    public float maxheight;

    public void StartRising()
    {
        StartCoroutine(Rise());
    }

    public void StopRising()
    {
        StopCoroutine(Rise());
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
