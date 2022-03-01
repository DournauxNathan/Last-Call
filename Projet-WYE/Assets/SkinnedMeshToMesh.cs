using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public VisualEffect VFXGraph;
    public ParticleSystem Particle;
    public ParticleSystem Particle2;

    public float refreshRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph()); 
    }

    IEnumerator UpdateVFXGraph()
    {
        while(gameObject.activeSelf)
        {
            Mesh m = new Mesh();
            /*skinnedMesh.BakeMesh();*/
            //VFXGraph.SetMesh("Mesh", m);

            Vector3[] vertices= m.vertices;
            Mesh m2 = new Mesh();
            m2.vertices = vertices;

            //VFXGraph.SetMesh("Mesh", m2);

            yield return new WaitForSeconds(refreshRate);
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
