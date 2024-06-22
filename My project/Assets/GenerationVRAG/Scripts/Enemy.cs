using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	[SerializeField] private float _speed;
	private float _maxRotation = 361f;
	private Rigidbody _rigidbody;

	public event Action<Enemy> CameOut;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		transform.Translate(Vector3.forward*_speed*Time.fixedDeltaTime, Space.Self);
	}

	public void SetStartSettings(Transform startPosition)
	{
		transform.position = startPosition.position;
		transform.Rotate(Vector3.up*Random.Range(0, _maxRotation));

		gameObject.SetActive(true);
		_rigidbody.velocity = Vector3.zero;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent<SafeZone>(out SafeZone _))		
			CameOut.Invoke(this);
	}
}
