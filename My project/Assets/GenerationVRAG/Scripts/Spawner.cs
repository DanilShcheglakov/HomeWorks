using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Enemy _prefabEnemy;
	[SerializeField] private Transform _target;

	[SerializeField] private int _poolCapacity = 10;
	[SerializeField] private int _poolMaxSize = 10;

	private ObjectPool<Enemy> _pool;

	private void Awake()
	{
		_pool = new ObjectPool<Enemy>(
			createFunc: () => Instantiate(_prefabEnemy),
			actionOnGet: (enemy) => enemy.SetStartSettings(transform, _target),
			actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
			actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize);
	}

	public void GetEnemy()
	{
		Enemy newEnemy = _pool.Get();

		newEnemy.CameOut += RealiseEnemy;
	}

	private void RealiseEnemy(Enemy enemy)
	{
		enemy.CameOut -= RealiseEnemy;

		_pool.Release(enemy);
	}
}
