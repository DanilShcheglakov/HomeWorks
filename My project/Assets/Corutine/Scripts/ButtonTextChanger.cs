using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class ButtonTextChanger : MonoBehaviour
{
	private const string TextStart = "Start";
	private const string TextStop = "Stop";

	[SerializeField] private Counter _counter;

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
