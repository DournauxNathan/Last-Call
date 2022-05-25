using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator m_animator;
    
    public void Open()
    {
        m_animator.SetBool("Open", true);
    }

    public void Close()
    {
        m_animator.SetBool("Open", false);
    }
}
