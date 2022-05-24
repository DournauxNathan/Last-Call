using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpriteSheetReader : Singleton<SpriteSheetReader>
{
    public Image animatedImageObj;

    public int speed;

    public int memoIndex;

    public Sprite[] memo1;
    public Sprite[] memo2;
    public Sprite[] memo3;
    public Sprite[] memo4;
    public Sprite[] memo5;
    private Sprite[][] memos;
    private Image image;

    private Coroutine _displaySouvenir;

    private void Start() {
        memos = new Sprite[][] { memo1, memo2, memo3, memo4, memo5 };
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void CallPlaySouvenirs()
    {
        this.CallWithDelay(DisplaySouvenir, 0.1f);
    }
    void DisplaySouvenir(){
        
        //Debug.Log("DisplaySouvenir: "+_displaySouvenir);
        if(_displaySouvenir == null)
        {
            ActivateImage();
            StartCoroutine(Cooldown(speed,memos[memoIndex]));
            _displaySouvenir = StartCoroutine(Souvenir(speed,memos[memoIndex],memoIndex));
        }

    }

    IEnumerator Souvenir(float delay, Sprite[] souvenir, int index){
        List<Sprite> souvenirList = new List<Sprite>(souvenir); 
        while(souvenirList.Count>0)
        {
            animatedImageObj.sprite = memos[index][memos[index].Length -(souvenirList.Count)];
            souvenirList.RemoveAt(souvenirList.Count-1);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Cooldown(float delay, Sprite[] souvenir){
        float _time = delay * souvenir.Length;
        yield return new WaitForSeconds(_time);
        _displaySouvenir = null; Debug.Log("Cooldown for Souvenir");
    }

    private void ActivateImage(){
        image.enabled = true;
    }

    public void DeSelected(){
        image.enabled = false;
    }

    public void UpdateMemoIndex(int value)
    {
        memoIndex = value;
    }
}
