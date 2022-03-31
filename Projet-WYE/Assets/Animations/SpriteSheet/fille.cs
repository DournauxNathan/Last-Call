using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fille : MonoBehaviour
{
    public Sprite[] animatedImages;
    public Image animatedImageObj;

    // Update is called once per frame
    void Update()
    {
        animatedImageObj.sprite = animatedImages[(int)(Time.time * 10) / animatedImages.Length];
    }
}
