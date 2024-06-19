using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
	[SerializeField] Spowner _spowner;
	[SerializeField] int _divisonChance;
	[SerializeField] private float _explosionForce;
	[SerializeField] private float _explosionRadius;

	private int _divisionChaceDivider =2;
	private int _scaleDdivider =2;

	public float ExplosionForce=> _explosionForce;
	public float ExplosionRadius=> _explosionRadius;

	private void OnMouseDown()
	{
		Explode();
		Destroy(gameObject);
	}

	private void Explode()
	{
		int divisionChanse = UnityEngine.Random.Range(0, 100);

		if (divisionChanse > _divisonChance)
			return;

		_spowner.Divide(gameObject.GetComponent<Cube>());
	}

	public void SetNewState()
	{
		transform.localScale /= _divisionChaceDivider;
		_divisonChance /= _scaleDdivider;
		gameObject.GetComponent<Renderer>().material.color = new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
;	}
}
