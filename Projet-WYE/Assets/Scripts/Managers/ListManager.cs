using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ListManager : Singleton<ListManager>
{
    public List<GameObject> hoveredInteractors;
    public List<GameObject> lockedInteractors;

    public void ClearList()
    {
        hoveredInteractors.Clear();
    }

    public void OnPressed()
    {
        if(hoveredInteractors.Count != 0 && hoveredInteractors[0].GetComponentInParent<Teleport>())
        {
            hoveredInteractors[0].GetComponentInParent<Teleport>().TeleportTo();
        }
        else if (hoveredInteractors.Count != 0 && !lockedInteractors.Contains(hoveredInteractors[0]))
        {
            Select();
        }
        else if (hoveredInteractors.Count != 0 && lockedInteractors.Contains(hoveredInteractors[0]))
        {
            UnSelect();
        }
    }

    public void Select()
    {
        if (lockedInteractors.Count == 0 && hoveredInteractors.Count > 0) //Fonctionne ! -> ajoute le premier objet si liste vide != null
        {
            lockedInteractors.Add(hoveredInteractors[0]);
            hoveredInteractors[0].GetComponent<CombinableObject>().Lock(true);
        }

        if (lockedInteractors.Count != 0 && hoveredInteractors.Count > 0 && !lockedInteractors.Contains(hoveredInteractors[0]))
        {
            lockedInteractors.Add(hoveredInteractors[0]);
            hoveredInteractors[0].GetComponent<CombinableObject>().Lock(true);
        }

        if (lockedInteractors.Count == 2)
        {
            CheckCompatibility(lockedInteractors[0], lockedInteractors[1]);

            for (int i = 0; i < lockedInteractors.Count; i++)
            {
                lockedInteractors[i].GetComponent<CombinableObject>().Lock(false);
            }
            lockedInteractors.Clear();
        }
    }

    public void UnSelect()
    {
        for (int i = 0; i < lockedInteractors.Count; i++)
        {
            lockedInteractors[i].GetComponent<CombinableObject>().Lock(false);
        }
        lockedInteractors.Clear();
    }


    public void CheckCompatibility(GameObject objet1,GameObject objet2)
    {
        CombinableObject combiObj1, combiObj2;

        if (objet1.TryGetComponent<CombinableObject>(out combiObj1) && objet2.TryGetComponent<CombinableObject>(out combiObj2))
        {
            foreach (CombineWith combinaison in combiObj1.useWith)
            {
                if (combinaison.objectName == combiObj2.name && combiObj1.state == StateMobility.Static)
                {
                    PlaySfx(combinaison.sfx,combiObj1);
                    combiObj2.dissolveEffect.startEffect = true;

                    SetToOrderController(combiObj1, combiObj2, combinaison.influence, combinaison.outcome, combinaison.isLethal);
                }
                else if (combinaison.objectName == combiObj2.name && combiObj2.state == StateMobility.Static)
                {
                    PlaySfx(combinaison.sfx,combiObj2);
                    combiObj1.dissolveEffect.startEffect = true;
                    SetToOrderController(combiObj1, combiObj2, combinaison.influence, combinaison.outcome, combinaison.isLethal);
                }
                else if (combinaison.objectName == combiObj2.name && combiObj1.state != StateMobility.Static)
                {
                    PlaySfx(combinaison.sfx,combiObj1);
                    combiObj1.dissolveEffect.startEffect = true;
                    combiObj2.dissolveEffect.startEffect = true;

                    SetToOrderController(combiObj1, combiObj2, combinaison.influence, combinaison.outcome, combinaison.isLethal);
                }
            }
        }
        else
        {
            Debug.LogWarning("No one has the combinaisons component");
        }       
    }

    public void SetToOrderController(CombinableObject objectA, CombinableObject objectB, int value, string _outcome, bool _lethality)
    {
        OrderController.Instance.AddCombinaison(objectA, objectB, value, _outcome, _lethality);
        //PlaytestData.Instance.betaTesteurs.data.numberOfCombinaisonsMade++;
        OrderController.Instance.ResolvePuzzle();
    }

    public void PlaySfx(AudioClip sfx, CombinableObject combiObj){
        AudioSource audio = combiObj.audioSource;
        audio.PlayNewClipOnce(sfx);
    }

}