using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorAnimation : MonoBehaviour
{

    public Animator ScissorAnimator;
    public bool open;
    public bool close;

    private void Update()
    {
        if (close)
        {
            Close();
        }
        else if (open)
        {
            Open();
        }
    }

    public void Close()
    {
        close = false;
        ScissorAnimator.SetBool("Close", true);
        open = true;
    }


    void Open()
    {
        open = false;
        ScissorAnimator.SetBool("Close", false);
    }
}
