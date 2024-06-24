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
		_protectedArea.StrangerCome += StartAlarm;
		_protectedArea.StrangerExit += StopAlarm;
	}

	private void OnDisable()
	{
		_protectedArea.StrangerCome -= StartAlarm;
		_protectedArea.StrangerExit -= StopAlarm;
	}

	private void StartAlarm()
	{
		if (_changeVolume != null)
			StopCoroutine(_changeVolume);

		if (_audioSource.isPlaying == false)
			_audioSource.Play();

		_changeVolume = StartCoroutine(ChangeVolume(_maxVolume));

	}

	private void StopAlarm()
	{
		if (_changeVolume != null)
			StopCoroutine(_changeVolume);

		_changeVolume = StartCoroutine(ChangeVolume(_minVolume));
	}

	private IEnumerator ChangeVolume(float targetVolume)
	{
		var delay = new WaitForFixedUpdate();

		while (_volume != targetVolume)
		{
			_volume = Mathf.MoveTowards(_volume, targetVolume, _deltaVolume * Time.fixedDeltaTime);
			_audioSource.volume = _volume;

			yield return delay;
		}

		if (_audioSource.isPlaying && _volume == _minVolume)
			_audioSource.Stop();
	}
}
