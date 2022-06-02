using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAudio : MonoBehaviour
{
    public AudioClip clip;
    public Transform A;
    public Transform B;

    public float interpolation;

    public AudioSource _audioSource;

    public bool doLerp;
    bool doOnce = true;

    private void Start()
    {
        StartCoroutine(Move());

    }

    private void Update()
    {

        if (doLerp)
        {
            transform.position = Vector3.Lerp(transform.position, A.position, interpolation * Time.deltaTime);
        }

        if (transform.position == A.position)
        {
            doLerp = false;
            StopAllCoroutines();
        }
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        while (transform.position != A.position)
        {
            _audioSource.PlayNewClipOnce(clip);
            yield return new WaitForSeconds(2f);
            doLerp = true;
        }
       
    }
}
