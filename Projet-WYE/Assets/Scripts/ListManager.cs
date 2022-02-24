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
        if(hoveredInteractors.Count != 0)
        {
            if (hoveredInteractors[0].name == "TeleportPoint")
            {
                hoveredInteractors[0].GetComponent<Teleport>().TeleportTo();
            }
            if (!lockedInteractors.Contains(hoveredInteractors[0]))
            {
                Select();
            }
            else if (lockedInteractors.Contains(hoveredInteractors[0]))
            {
                UnSelect();
            }
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
        CombinableObject _objectManager1, _objectManager2;

        if (objet1.TryGetComponent<CombinableObject>(out _objectManager1) && objet2.TryGetComponent<CombinableObject>(out _objectManager2))
        {
            Debug.Log("A" + _objectManager1.name + " | " + _objectManager2.name);

            //Check with 2 objects only and with the ObjectManager1
            if (_objectManager1.combineWith.Count == 2 && _objectManager1.combineWith[0] == objet2.name.ToString()  
                || _objectManager1.combineWith.Count == 2 && _objectManager1.combineWith[1] == objet2.name.ToString())
            {
                Debug.Log("B" + _objectManager1.name + " | " + _objectManager2.name);
                if (_objectManager1.state == StateMobility.Static)
                {
                    Debug.Log("C Static" + _objectManager1.name + " | " + _objectManager2.name);
                    objet2.SetActive(false);

                    //Clear the list if a combinaison has already been find
                    _objectManager1.combineWith.Clear();
                }
                else if (_objectManager2.state == StateMobility.Static)
                {
                    Debug.Log("Cbis Static" + _objectManager1.name + " | " + _objectManager2.name);
                    objet1.SetActive(false);

                    //Clear the list if a combinaison has already been find
                    _objectManager1.combineWith.Clear();
                }

                else
                {
                    Debug.Log("Suppr1");
                    objet1.SetActive(false);
                    objet2.SetActive(false);
                }

                SetToOrderController(_objectManager1, _objectManager2);
            }
            else if (_objectManager2.combineWith.Count == 2 && _objectManager2.combineWith[0] == objet1.name.ToString() && _objectManager1.combineWith.Count != 0 
                || _objectManager2.combineWith.Count == 2 && _objectManager2.combineWith[0] == objet1.name.ToString() && _objectManager1.combineWith.Count != 0)
            {
                Debug.Log("D" + _objectManager1.name + " | " + _objectManager2.name);
                if (_objectManager1.state == StateMobility.Static)
                {
                    Debug.Log("C Static" + _objectManager1.name + " | " + _objectManager2.name);
                    objet2.SetActive(false);

                    //Clear the list if a combinaison has already been find
                    _objectManager1.combineWith.Clear();
                }
                
                //Check with 2 objects only and with the ObjectManager2
                else if (_objectManager2.state == StateMobility.Static)
                {
                    Debug.Log("E Static" + _objectManager1.name + " | " + _objectManager2.name);
                    objet1.SetActive(false);

                    //Clear the list if a combinaison has already been find
                    _objectManager2.combineWith.Clear();
                }
                else
                {
                    Debug.Log("Suppr2");
                    objet1.SetActive(false);
                    objet2.SetActive(false);
                }

                SetToOrderController(_objectManager2, _objectManager1);
            }
            else if (_objectManager1.combineWith.Count == 1 && _objectManager2.combineWith.Count != 0 
                || _objectManager2.combineWith.Count == 1 && _objectManager1.combineWith.Count != 0)
            {
                //Check with 1 object only
                if (_objectManager1.combineWith[0] == objet2.ToString())
                {
                    if (_objectManager1.state == StateMobility.Static)
                    {
                        objet2.SetActive(false);

                        //Clear the list if a combinaison has already been find
                        _objectManager1.combineWith.Clear();
                    }
                    else
                    {
                        Debug.Log("Suppr3");
                        objet1.SetActive(false);
                        objet2.SetActive(false);
                    }

                    SetToOrderController(_objectManager1, _objectManager2);
                }
                else if (_objectManager2.combineWith[0] == _objectManager1.name)
                {
                    if (_objectManager2.state == StateMobility.Static)
                    {
                        objet1.SetActive(false);

                        //Clear the list if a combinaison has already been find
                        _objectManager2.combineWith.Clear();
                    }
                    else
                    {
                        Debug.Log("Suppr4");
                        objet1.SetActive(false);
                        objet2.SetActive(false);
                    }

                    SetToOrderController(_objectManager2, _objectManager1);
                }
            }            
            else if (_objectManager1.combineWith.Count == 0 || _objectManager2.combineWith.Count == 0)
            {
                Debug.LogWarning("Can't combine them");
            }
        }
        else
        {
            Debug.LogWarning("No one has the combinaisons component");
        }       
    }

    public void SetToOrderController(CombinableObject _objectManagerA, CombinableObject _objectManagerB)
    {
        OrderController.Instance.AddCombinaison(_objectManagerA, _objectManagerB);
        PlaytestData.Instance.betaTesteurs.data.numberOfCombinaisonsMade++;
        OrderController.Instance.IncreaseValue(1);
        
        /*
        if (!OrderController.Instance.orders.Contains(_objectManager.resultOrder))
        {
            OrderController.Instance.orders.Add(_objectManager.resultOrder);
            OrderController.Instance.IncreaseValue(1);
        }*/
    }
}