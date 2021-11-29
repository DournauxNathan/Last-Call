using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Unit
{
    None,
    Police,
    SWAT,
    FireDepartment,
    FAS,
    All
}

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public List<Unit> units;

    public List<PhysicsButton> physicsbuttons;

    public bool unitUnlock = false;
    private void Start()
    {
        instance = this;

        physicsbuttons.AddRange(GameObject.FindObjectsOfType<PhysicsButton>());
    }

    public  void AddToUnlock(Unit unit) 
    {
        List<Unit> tempUnit = new List<Unit>();
        tempUnit.Add(Unit.FAS);
        tempUnit.Add(Unit.FireDepartment);
        tempUnit.Add(Unit.SWAT);
        tempUnit.Add(Unit.Police);

        if (unit != Unit.None && !units.Contains(unit))
        {
            units.Add(unit);
        }

        if (unit == Unit.All)
        {
            units.Clear();
            units.AddRange(tempUnit);
            
        }
        UnlockPhysicsButton(unit);
    }

    public void UnlockPhysicsButton(Unit unit)
    {
        if(unit == Unit.All)
        {
            foreach (var button in physicsbuttons)
            {
                button.isActivate = true;
            }
        }
        else 
        {
            foreach (var button in physicsbuttons)
            {
                if (button.unitToSend == unit)
                {
                    button.isActivate = true;
                }
            }
        }
    }
}
