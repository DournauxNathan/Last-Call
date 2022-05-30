using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extention
{
    public static Vector3 FromVector2(this Vector2 v, float z)
    {
        return new Vector3(v.x,v.y,z);
    }
}
