using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gun : MonoBehaviour
{
	private Transform _target;
	[SerializeField] private Rigidbody _prefabBullet;

	[SerializeField] private float _speed;
	[SerializeField] private float _delay;

	private void Start()
	{
		StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
		var delay = new WaitForSeconds(_delay);

		while (enabled)
		{
			Vector3 direction = (_target.position - transform.position).normalized;

			Rigidbody rigidBodyNewBullet = Instantiate(_prefabBullet, transform.position + direction, Quaternion.identity);

			rigidBodyNewBullet.transform.up = direction;
			rigidBodyNewBullet.velocity = direction * _speed;

			yield return delay;
		}
	}
}