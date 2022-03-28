using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ShakeWord : MonoBehaviour
{
    [Header("info")]
    private Vector3 _startPos;
    [SerializeField]private float _timer;
    private Vector3 _randomPos;
    

    private bool isGrabed = false;

    [Header("Settings")]
    [Range(0f, 2f)] public float _time = 0.2f;
    [Range(0f, 2f)] public float _distance = 0.1f;
    [Range(0f, 0.1f)] public float _delayBetweenShakes = 0f;

	private void Awake()
	{
		_startPos = transform.parent.localPosition;
	}

    private void Start()
    {
		Begin();
    }

    private void OnValidate()
	{
		if (_delayBetweenShakes > _time)
			_delayBetweenShakes = _time;
	}

	public void Begin()
	{
		StopAllCoroutines();
		StartCoroutine(Shake());
	}

	private IEnumerator Shake()
	{
		_timer = 0f;

		while (_timer <= _time)
		{
			_timer += Time.deltaTime;

			_randomPos = transform.parent.localPosition + (Random.insideUnitSphere * _distance);
			_randomPos.z = _startPos.z;

			transform.position = _randomPos;

			if (_delayBetweenShakes > 0f)
			{
				yield return new WaitForSeconds(_delayBetweenShakes);
			}
			else
			{
				yield return null;
			}
		}

		transform.position = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y,_startPos.z);
	}


}
