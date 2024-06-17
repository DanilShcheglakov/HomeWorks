using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Counter : MonoBehaviour
{
	[SerializeField] private float _delay;
	[SerializeField] private int _stepValue;

	private TextMeshProUGUI _text;
	private bool _isWork = false;
	private int _currentValue;

	public event Action StatusChanged;

	public bool IsWork => _isWork;

	private void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
		_text.text = "0";
	}

	public void ChangeStatus()
	{
		if (_isWork == false)
		{
			_isWork = true;
			StartCoroutine(ChangeValue());
		}
		else
			_isWork = false;

		StatusChanged.Invoke();
	}

	private IEnumerator ChangeValue()
	{
		var wait = new WaitForSeconds(_delay);

		while (_isWork)
		{
			_currentValue += _stepValue;
			_text.text = _currentValue.ToString();

			yield return wait;
		}
	}
}
