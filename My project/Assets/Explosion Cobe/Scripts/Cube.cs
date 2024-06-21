using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
	[SerializeField] private Spawner _spawner;
	[SerializeField] private Explode _exploder;
	[SerializeField] private int _divisonChance;
	[SerializeField] private float _explosionForce;
	[SerializeField] private float _explosionRadius;

	private int _divisionChaceDivider = 2;
	private int _scaleDdivider = 2;
	private float _explosionForceMultiply = 1.5f;
	private float _explosionRadiusMultiply = 1.5f;

	public float ExplosionForce => _explosionForce;
	public float ExplosionRadius => _explosionRadius;

	public event Action<Cube> Clicked;

	private void OnMouseDown()
	{
		Divide();
		_exploder.Run(this);
		Destroy(gameObject);
	}

	public void SetNewState()
	{
		transform.localScale /= _divisionChaceDivider;
		_divisonChance /= _scaleDdivider;
		_explosionForce *= _explosionForceMultiply;
		_explosionRadius *= _explosionRadiusMultiply;

		GetComponent<Renderer>().material.color = new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}

	private void Divide()
	{
		int divisionChanse = UnityEngine.Random.Range(0, 100);

		if (divisionChanse <= _divisonChance)
			_spawner.Divide(this);		
	}
}
