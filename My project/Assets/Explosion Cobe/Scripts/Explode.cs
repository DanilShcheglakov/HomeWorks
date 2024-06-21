using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
	[SerializeField] private float _explosionForce;
	[SerializeField] private float _explosionRadius;

	private float _explosionForceMultiply = 1.5f;
	private float _explosionRadiusMultiply = 1.5f;

	public void Run(Cube explosionCube)
	{
		foreach (Rigidbody exploadableCube in GetExplodableObjects(explosionCube))
			exploadableCube.AddExplosionForce(_explosionForce, explosionCube.transform.position, _explosionRadius);
	}

	public void Run(Cube exploadableCube, Vector3 position)
	{
		if (exploadableCube.TryGetComponent<Rigidbody>(out Rigidbody exploadableCubeRigidBody))
			exploadableCubeRigidBody.AddExplosionForce(_explosionForce, position, _explosionRadius);
	}

	public void SetNewState()
	{
		_explosionForce *= _explosionForceMultiply;
		_explosionRadius *= _explosionRadiusMultiply;
	}

	private List<Rigidbody> GetExplodableObjects(Cube explosionCube)
	{
		Collider[] explodableColliders = Physics.OverlapSphere(explosionCube.transform.position, _explosionRadius);

		List<Rigidbody> explodableRigidbodies = new();

		foreach (Collider explodableCollider in explodableColliders)
			if (explodableCollider.attachedRigidbody != null)
				explodableRigidbodies.Add(explodableCollider.attachedRigidbody);

		return explodableRigidbodies;
	}
}
