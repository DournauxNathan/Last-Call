using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlockEffect : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform calculatedTransform;
    public float distanceFromCamera;
    public float time;
    public List<Transform> words;
    [SerializeField] private float minOffsetFloat;
    [SerializeField] private float maxOffsetFloat;

    private void Start() {
        words = new List<Transform>();
    }

    public void SetUp()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            words.Add(transform.GetChild(i));
        }
        AddOffSet(); 
    }
    public void ClearList()
    {
        words.Clear();
    }

    void FixedUpdate()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
        transform.rotation = cameraTransform.rotation;

        calculatedTransform = transform;
    }

    void AddOffSet()
    {
        for (var i = 0; i < words.Count; i++)
        {
            words[i].position += new Vector3(Random.Range(minOffsetFloat, maxOffsetFloat), Random.Range(minOffsetFloat, maxOffsetFloat), Random.Range(minOffsetFloat, maxOffsetFloat));
        }        
    }
}
