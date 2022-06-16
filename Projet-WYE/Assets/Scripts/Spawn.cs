using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float destroyTime = 5;
    public List<GameObject> list;

    public int numberlist = 0;

    void Start()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }

        StartCoroutine("CoroutineSpawn");
    }

    void Update()
    {

    }

    IEnumerator CoroutineSpawn()
    {
        list[numberlist].SetActive(true);

        yield return new WaitForSeconds(5);
        list[numberlist].SetActive(false);
        numberlist++;
        if (numberlist >= list.Count)
        {
            StopCoroutine("CoroutineSpawn");
        }
        else
        {
            StartCoroutine("CoroutineSpawn");
        }
    }
}
