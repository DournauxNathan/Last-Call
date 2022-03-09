using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDissolveTarget : Singleton<DistanceDissolveTarget>
{ 
    public List<Transform> m_objectToTrack;

    private Material m_materialRef = null;

    private void Start()
    {
        if (m_objectToTrack.Count <= 3)
        {
            MaterialRef.SetVector("_Position", m_objectToTrack[0].position);
            MaterialRef.SetVector("_Position_1", m_objectToTrack[1].position);
            MaterialRef.SetVector("_Position_2", m_objectToTrack[2].position);
        }
        else if (m_objectToTrack.Count == 3)
        {
            MaterialRef.SetVector("_Position", m_objectToTrack[0].position);
            MaterialRef.SetVector("_Position_1", m_objectToTrack[1].position);
            MaterialRef.SetVector("_Position_2", m_objectToTrack[2].position);
            MaterialRef.SetVector("_Position_3", m_objectToTrack[3].position);
        }

        if (Projection.Instance.setWallWithOutline)
        {
            SetObjectToTrack();
        }
    }

    public void SetObjectToTrack()
    {
        MaterialRef.SetVector("_Position", m_objectToTrack[0].position);
        MaterialRef.SetVector("_Position_1", m_objectToTrack[1].position);
        MaterialRef.SetVector("_Position_2", m_objectToTrack[2].position);
        MaterialRef.SetVector("_Position_3", m_objectToTrack[3].position);
    }

    public Material MaterialRef
    {
        get 
        {
            if (m_materialRef == null)
            {
                m_materialRef = GetComponent<Renderer>().material;
            }

            return m_materialRef;
        }
    }

    private void Update()
    {
        if (m_objectToTrack.Count <= 3)
        {
            MaterialRef.SetVector("_Position", m_objectToTrack[0].position);
            MaterialRef.SetVector("_Position_1", m_objectToTrack[1].position);
            MaterialRef.SetVector("_Position_2", m_objectToTrack[2].position);
        }
        else if (m_objectToTrack.Count == 3)
        {
            MaterialRef.SetVector("_Position", m_objectToTrack[0].position);
            MaterialRef.SetVector("_Position_1", m_objectToTrack[1].position);
            MaterialRef.SetVector("_Position_2", m_objectToTrack[2].position);
            MaterialRef.SetVector("_Position_3", m_objectToTrack[3].position);
        }
    }
}