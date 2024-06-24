using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
	[SerializeField] private ProtectedArea _protectedArea;
	[SerializeField] private float _deltaVolume;

	private Coroutine _changeVolume;

	private AudioSource _audioSource;
	private float _volume = 0f;
	private float _maxVolume = 1f;
	private float _minVolume = 0f;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.Stop();
	}

	private void OnEnable()
	{
		_protectedArea.StrangerCome += ChangeAlarmSettings;
		_protectedArea.StrangerExit += ChangeAlarmSettings;
	}

	private void OnDisable()
	{
		_protectedArea.StrangerCome -= ChangeAlarmSettings;
		_protectedArea.StrangerExit -= ChangeAlarmSettings;
	}

	private void ChangeAlarmSettings(bool isCrookInside)
	{
		float targetVolume;

		if (isCrookInside)
		{
			if (_audioSource.isPlaying == false)
				_audioSource.Play();

			targetVolume = _maxVolume;
		}
		else
		{
			targetVolume = _minVolume;
		}

		if (_changeVolume != null)
			StopCoroutine(_changeVolume);

		_changeVolume = StartCoroutine(ChangeVolume(targetVolume));
	}

	private IEnumerator ChangeVolume(float targetVolume)
	{
		var delay = new WaitForFixedUpdate();

		while (_volume <= 1 && _volume >= 0)
		{
			_volume = Mathf.MoveTowards(_volume, targetVolume, _deltaVolume * Time.fixedDeltaTime);
			_audioSource.volume = _volume;

			yield return delay;
		}

		if (_audioSource.isPlaying && _volume == _minVolume)
			_audioSource.Stop();
	}
}
