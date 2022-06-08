using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteData : MonoBehaviour
{
    public bool isActive = false;

    public Silhouette identity = new Silhouette();
    private void Start() {
        if(identity.id != -1){
            gameObject.SetActive(!identity.SendPresence(gameObject));
            TryGetComponent<SilhouetteCanvas>(out SilhouetteCanvas canvas);
            if(canvas != null) Destroy(canvas);
        }
    }

    public void OnEnable()
    {
        if (isActive)
        {
            WayPointSound.Instance.ChangeLocation();
        }
        else
        {
            //WayPointSound.Instance.ChangeLocation();
            isActive = true;
        }
    }

    private void Update() {
    }

// Set in XRGrab event OnHoverEnter
    public void OnHoverEnter()
    {
       
    }

    public void OnClick(){
        //identity.outcomeLink.OnValidate(); // call the OnValidate function of the outcomeLink
    }

    //will be call when the user cancel a word otherwise words will stay in the world
    public void OnCancel()
    {
        
    }  
}
