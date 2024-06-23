using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	[SerializeField] private float _speed;
	private Rigidbody _rigidbody;
	private Transform _target;

	public event Action<Enemy> CameOut;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
	}

	public void SetStartSettings(Transform startPosition, Transform target)
	{
		transform.position = startPosition.position;
		_target = target;
		transform.LookAt(_target);

		gameObject.SetActive(true);
		_rigidbody.velocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<Target>(out Target _))
			CameOut.Invoke(this);
	}
}
