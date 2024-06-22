using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
	[SerializeField] private float _maxLifeTime = 5;
	[SerializeField, Min(1f)] private float _minLifeTime = 2;
	[SerializeField] private Material _defaultMaterial;

	private bool _wasCollision = false;

	private Rigidbody _rigidBody;
	private Renderer _renderer;

	public event Action<Cube> OnDied;

	private void OnValidate()
	{
		if (_minLifeTime >= _maxLifeTime)
			_maxLifeTime = _minLifeTime + 1;
	}

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_renderer = GetComponent<Renderer>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (_wasCollision == false && collision.gameObject.TryGetComponent<Ground>(out Ground _))
		{
			_wasCollision = true;

			SetNewState();
			StartCoroutine(Die());
		}
	}

	public void SetDefaulSettings(Vector3 newPosition)
	{
		_wasCollision = false;
		_renderer.material = _defaultMaterial;
		transform.position = newPosition;
		_rigidBody.velocity = Vector3.zero;
		gameObject.SetActive(true);
	}

	public void SetNewState()
	{
		_renderer.material.color = new Color(Random.value, Random.value, Random.value);
	}

	private IEnumerator Die()
	{
		yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));

		OnDied.Invoke(this);
	}
}