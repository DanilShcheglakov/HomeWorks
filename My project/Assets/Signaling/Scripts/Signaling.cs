using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
	[SerializeField] private ProtectedArea _protectedArea;
	[SerializeField] private float _deltaVolume;

	private Coroutine _volumeUp;
	private Coroutine _volumeDowm;

	private AudioSource _audioSource;
	private float _volume = 0f;

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
		if (_volumeDowm != null)
			StopCoroutine(_volumeDowm);

		if (_audioSource.isPlaying == false)
			_audioSource.Play();

		_volumeUp = StartCoroutine(UpVolume());

	}

	private void StopAlarm()
	{
		if (_volumeUp != null)
			StopCoroutine(_volumeUp);

		_volumeDowm = StartCoroutine(DownVolume());
	}

	private IEnumerator UpVolume()
	{
		var delay = new WaitForFixedUpdate();

		while (_volume < 1)
		{
			_volume = Mathf.MoveTowards(_volume, 1, _deltaVolume * Time.fixedDeltaTime);
			_audioSource.volume = _volume;
			yield return delay;
		}
	}

	private IEnumerator DownVolume()
	{
		var delay = new WaitForFixedUpdate();

		while (_volume > 0)
		{
			_volume = Mathf.MoveTowards(_volume, 0, _deltaVolume * Time.fixedDeltaTime);
			_audioSource.volume = _volume;
			yield return delay;
		}

		_audioSource.Stop();
	}
}
