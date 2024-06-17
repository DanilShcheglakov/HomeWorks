using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonText : MonoBehaviour
{
	[SerializeField] Counter _counter;

	private const string TextStart = "Start";
	private const string TextStop = "Stop";

	private Text _currentText;

	private void Start()
	{
		_currentText = GetComponent<Text>();
	}

	private void OnEnable()
	{
		_counter.StatusChanged += ChangeText;
	}

	private void OnDisable()
	{
		_counter.StatusChanged -= ChangeText;
	}

	private void ChangeText()
	{
		if (_counter.IsWork)
			_currentText.text = TextStop;
		else
			_currentText.text = TextStart;
	}
}
