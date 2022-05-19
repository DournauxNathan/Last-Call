using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetReader : MonoBehaviour
{
    public Image animatedImageObj;

    public int speed;

    public int memoIndex;

    public Sprite[] memo1;
    public Sprite[] memo2;
    public Sprite[] memo3;
    public Sprite[] memo4;
    public Sprite[] memo5;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (memoIndex)
        {
            case 1:
                for (int i = 0; i < memo1.Length; i++)
                {
                    animatedImageObj.sprite = memo1[(int)(Time.time * speed) / memo1.Length];
                }
                break;
            case 2:
                for (int i = 0; i < memo2.Length; i++)
                {
                    animatedImageObj.sprite = memo2[(int)(Time.time * speed) / memo2.Length];
                }
                break;
            case 3:
                for (int i = 0; i < memo3.Length; i++)
                {
                    animatedImageObj.sprite = memo3[(int)(Time.time * speed) / memo3.Length];
                }
                break;
            case 4:
                for (int i = 0; i < memo4.Length; i++)
                {
                    animatedImageObj.sprite = memo4[(int)(Time.time * speed) / memo4.Length];
                }
                break;
            case 5:
                for (int i = 0; i < memo5.Length; i++)
                {
                    animatedImageObj.sprite = memo5[(int)(Time.time * speed) / memo5.Length];
                }
                break;
        }
    }
}
