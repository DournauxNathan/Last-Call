using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetReader : MonoBehaviour
{
    public Image animatedImageObj;

    public float speed;
    public Sprite[] animatedImages;

    // Update is called once per frame
    void FixedUpdate()
    {
        animatedImageObj.sprite = animatedImages[(int)(Time.time * speed) / animatedImages.Length];
    }
}
