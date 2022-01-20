using UnityEngine;

public class CoiledWireController : MonoBehaviour
{
    public float thickness = 0.8f;

    Material mat;

    private void Awake()
    {
        //rope = GetComponent<ObiRope>();
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // mat.SetFloat("_CableThickness", rope.restLength / rope.CalculateLength()  * thickness);
    }
}
