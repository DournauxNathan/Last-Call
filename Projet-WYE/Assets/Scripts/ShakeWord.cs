using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Animations;

public class ShakeWord : MonoBehaviour
{
    [Header("Param")]
    [Range(0f, 2f)] public float delayBeforAnim = 0f;
    private bool isStarted;
    [SerializeField] Animator m_animator;
    private float _time;
    private void Start()
    {
        _time = delayBeforAnim;
        //m_animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (_time>0)
        {
            _time -= Time.deltaTime;
        }

        else if (!isStarted && _time<=0)
        {
            isStarted = true;
            m_animator.SetBool("Bool",true);
        }

    }

}
