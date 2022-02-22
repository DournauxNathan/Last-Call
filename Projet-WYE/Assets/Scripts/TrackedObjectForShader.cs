using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObjectForShader : MonoBehaviour
{
    [Range(0.1f,1f)]
    public float range = 0.5f;
    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, range);
    }
}
