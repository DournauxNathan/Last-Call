using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Unit
{
    None = 0,
    EM = 1,
    Police = 2,
    FireDepartment = 3,
    SWAT = 4,
    All = 5
}

public class UnitDispatcher :  Singleton<UnitDispatcher>
{
    [HideInInspector] public  Unit unitEnum;

    public List<Unit> units;

    public List<PhysicsButton> physicsbuttons;

    public bool unitUnlock = false;

    public List<Unit> unitsSend;

    public int sequence;

    public void UpdateUI()
    {
        if (sequence == 1)
        {
            UIManager.Instance.UpdateUnitManager(1);
            
            foreach (var button in physicsbuttons)
            {
                button.isActivate = false;
            }

            StartCoroutine(SequenceManager(5f));
        }
        else if (sequence == 3)
        {
            UIManager.Instance.UpdateUnitManager(sequence);

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

        UIManager.Instance.UpdateUnitManager(sequence);
    }

    public void LoadUnitSequence()
    {
        UIManager.Instance.UpdateUnitManager(SaveQuestion.Instance.sequenceUnit);
    }
}
