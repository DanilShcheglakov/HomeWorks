using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Explode _exploder;

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

		int shards = UnityEngine.Random.Range(_minCubesCopies, _maxCubesCopies);

		for (int i = 0; i < shards; i++)
		{
			copy = Instantiate(cube, newPosition, Quaternion.identity);
			copy.SetNewState();

			_exploder.Run(copy, cube.transform.position);
		}
	}
}
