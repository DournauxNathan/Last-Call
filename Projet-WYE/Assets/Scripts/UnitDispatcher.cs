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

public class UnitDispatcher :  Singleton<UnitDispatcher>
{
    public List<Unit> units;

    public List<PhysicsButton> physicsbuttons;

    public bool unitUnlock = false;

    public List<Unit> unitsSend;

    public int sequence;

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

    public void UpdateUI()
    {
        if (sequence == 1)
        {
            /*for (int i = 0; i < UIManager.Instance.checkListTransform.childCount; i++)
            {
                UIManager.Instance.checkListTransform.GetChild(i).GetComponent<InstantiableButton>().button.enabled = false;
            }*/

            UiTabSelection.Instance.SwitchSequence(1);
            
            foreach (var button in physicsbuttons)
            {
                button.isActivate = false;
            }

            StartCoroutine(SequenceManager(5f));
        }
        else if (sequence == 3)
        {
            UiTabSelection.Instance.SwitchSequence(sequence);
            StartCoroutine(SequenceManager(10f));
        }
    }

    public void NextSequence()
    {
        sequence++;
    }

    IEnumerator SequenceManager(float time)
    {        
        yield return new WaitForSeconds(time);
        NextSequence();

        if (sequence == 2)
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
                button.isActivate = false;
            }
        }

        UiTabSelection.Instance.SwitchSequence(sequence);
    }

    public void LoadUnitSequence()
    {
        UiTabSelection.Instance.SwitchSequence(SaveQuestion.Instance.sequenceUnit);
    }
}
