using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetReader : MonoBehaviour
{
    public Image animatedImageObj;

    public int speed;
    public Sprite[] animatedImages;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < animatedImages.Length; i++)
        {
            animatedImageObj.sprite = animatedImages[(int)(Time.time * speed) / animatedImages.Length];
        }
    }
}
