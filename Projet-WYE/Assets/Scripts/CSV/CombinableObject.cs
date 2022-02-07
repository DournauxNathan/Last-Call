using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinableObject_Data
{
    public string Name;
    public int Hp;
    public int Dammage;
    public string Fruit;

    public void Init(string[] entry)
    {
        Name = entry[0];
        Hp= int.Parse(entry[1]);
        Dammage = int.Parse(entry[2]);
        Fruit = entry[3];
    }
}
