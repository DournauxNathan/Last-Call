using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] SphereCollider sphereCol;
    [SerializeField] int number;

    Vector3 sphereSize;
    Vector3 sphereCenter;

    private void Awake()
    {
        Transform sphereTrans = sphereCol.GetComponent<Transform>();
        sphereCenter = sphereTrans.position;

        // Multiply by scale because it does affect the size of the collider
        sphereSize.x = sphereTrans.localScale.x * sphereCol.bounds.size.x;
        sphereSize.y = sphereTrans.localScale.y * sphereCol.bounds.size.y;
        sphereSize.z = sphereTrans.localScale.z * sphereCol.bounds.size.z;
    }


    private void Start()
    {
        for (int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        }
    }


    private Vector3 GetRandomPosition()
    {
        // You can also take off half the bounds of the thing you want in the box, so it doesn't extend outside.
        // Right now, the center of the prefab could be right on the extents of the box
        Vector3 randomPosition = new Vector3(Random.Range(-sphereSize.x / 2, sphereSize.x / 2), Random.Range(-sphereSize.y / 2, sphereSize.y / 2), Random.Range(-sphereSize.z / 2, sphereSize.z / 2));

        return sphereCenter + randomPosition;
    }
}