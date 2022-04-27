using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Rising : MonoBehaviour
{
    public GameObject water;
    public float time;
    public float maxheight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(water.transform.position.y!=maxheight)
        {
            Debug.Log("Rising");
            water.transform.position = new Vector3(water.transform.position.x,  Mathf.Lerp(water.transform.position.y, maxheight, time * Time.deltaTime), water.transform.position.z);
        }
    }
}
