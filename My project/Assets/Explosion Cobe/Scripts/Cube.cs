using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
	[SerializeField] private Spawner _spawner;
	[SerializeField] private int _divisonChance;
	[SerializeField] private float _explosionForce;
	[SerializeField] private float _explosionRadius;

	private int _divisionChaceDivider = 2;
	private int _scaleDdivider = 2;

	public float ExplosionForce => _explosionForce;
	public float ExplosionRadius => _explosionRadius;

	private void OnMouseDown()
	{
		Explode();
		Destroy(gameObject);
	}

	public void SetNewState()
	{
		transform.localScale /= _divisionChaceDivider;
		_divisonChance /= _scaleDdivider;

		GetComponent<Renderer>().material.color = new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}

	private void Explode()
	{
		int divisionChanse = UnityEngine.Random.Range(0, 100);

		if (divisionChanse > _divisonChance)
			return;

		_spawner.Divide(this);
	}
}
