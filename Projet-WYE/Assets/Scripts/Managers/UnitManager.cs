using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Unit
{
    None = 0,
    EMS = 1,
    Police = 2,
    FireDepartment = 3,
    SWAT = 4,
    All = 5
}

public class UnitManager :  Singleton<UnitManager>
{
    public List<PhysicsButton> physicsbuttons;

    public List<Unit> unitsSend;

    private int sequence;

    public void UpdateUI()
    {
        if (sequence == 1)
        {
            UIManager.Instance.UpdateUnitManager(1);
            
            foreach (var button in physicsbuttons)
            {
                button.ChangeStateColor(false);
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
                button.ChangeStateColor(true);
            }
        }

        UIManager.Instance.UpdateUnitManager(sequence);
    }

    public void LoadUnitSequence()
    {
        UIManager.Instance.UpdateUnitManager(SaveQuestion.Instance.sequenceUnit);
    }
}
