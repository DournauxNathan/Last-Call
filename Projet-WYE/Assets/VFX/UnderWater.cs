using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderWater : MonoBehaviour
{
    public GameObject PostProcessActuel;
    public GameObject PostProcessAquatique;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessAquatique.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > MasterManager.Instance.references.mainCamera.position.y )
        {
            PostProcessActuel.SetActive(false);
            PostProcessAquatique.SetActive(true);
        }
        else
        {
            PostProcessActuel.SetActive(true);
            PostProcessAquatique.SetActive(false);
        }
    }



}
