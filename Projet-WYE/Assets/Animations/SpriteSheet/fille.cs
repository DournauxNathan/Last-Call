using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fille : MonoBehaviour
{
    public float _Time;
    public Sprite[] animatedImages;
    public Image animatedImageObj;

    // Update is called once per frame
    void Update()
    {
        animatedImageObj.sprite = animatedImages[(int)(Time.time * _Time) / animatedImages.Length];
    }
}
