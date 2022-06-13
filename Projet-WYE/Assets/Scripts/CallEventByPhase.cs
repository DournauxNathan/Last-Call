using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEventByPhase : Singleton<CallEventByPhase>
{
    public UnityEvent onPhase0, onPhase1, onPhase2, onPhase3, onPhase4;

    private void Update()
    {
        CallEvent((int)MasterManager.Instance.currentPhase);
    }

    public void CallEvent(int i)
    {
        //Debug.Log((int)MasterManager.Instance.currentPhase);
        switch (i)
        {
            case 0:
                onPhase0?.Invoke();
                break;

            case 1:
                onPhase1?.Invoke();
                break;

            case 2:
                onPhase2?.Invoke();
                break;

            case 3:
                onPhase3?.Invoke();
                break;

            case 4:
                onPhase4?.Invoke();
                break;
        }
    }
}
