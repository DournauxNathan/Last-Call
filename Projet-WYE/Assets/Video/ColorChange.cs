using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{

    float time = 1.07f;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
