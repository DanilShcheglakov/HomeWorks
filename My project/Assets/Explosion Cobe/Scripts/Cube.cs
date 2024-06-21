using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
	[SerializeField] private Spawner _spawner;
	[SerializeField] private Explode _exploder;
	[SerializeField] private int _divisonChance;

	private int _divisionChaceDivider = 2;
	private int _scaleDdivider = 2;

	public event Action<Cube> Clicked;

	private void OnMouseDown()
	{
		Divide();
		Destroy(gameObject);
	}

	public void SetNewState()
	{
		transform.localScale /= _divisionChaceDivider;
		_divisonChance /= _scaleDdivider;

		GetComponent<Renderer>().material.color = new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

		_exploder.SetNewState();
	}

	private void Divide()
	{
		int divisionChanse = UnityEngine.Random.Range(0, 100);

		if (divisionChanse <= _divisonChance)
			_spawner.Divide(this);
		else
			_exploder.Run(this);
	}
}
