using UnityEngine;

public class Spowner : MonoBehaviour
{
	[SerializeField, Min(0)] private int _minCubesCopies;
	[SerializeField] private int _maxCubesCopies;

	private void OnValidate()
	{
		if (_maxCubesCopies <= _minCubesCopies)
			_maxCubesCopies = _minCubesCopies + 1;
	}

	public void Divide(Cube cube)
	{
		Cube copy;

		Vector3 newPosition = cube.transform.position;

		int shards = Random.Range(_minCubesCopies, _maxCubesCopies);

		for (int i = 0; i < shards; i++)
		{
			copy = Instantiate(cube, newPosition, Quaternion.identity);
			copy.SetNewState();

			copy.GetComponent<Rigidbody>().AddExplosionForce(cube.ExplosionForce, cube.transform.position, cube.ExplosionRadius);
		}
	}
}
