using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Enemy _prefabEnemy;
	[SerializeField] private Transform _startPonints;

	[SerializeField] private int _poolCapacity = 10;
	[SerializeField] private int _poolMaxSize = 10;
	[SerializeField] private float _reapitingRate = 2f;

	private List<Transform> _spawnPointsTransform = new();
	private ObjectPool<Enemy> _pool;

	private void Awake()
	{
		FillStartPoints();

		_pool = new ObjectPool<Enemy>(
			createFunc: () => Instantiate(_prefabEnemy),
			actionOnGet: (enemy) => enemy.SetStartSettings(GetSpawnPoint()),
			actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
			actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize);

		StartCoroutine(GenerateEnemyes());
	}

	private void GetEnemy()
	{
		Enemy newEnemy = _pool.Get();

		newEnemy.CameOut += RealiseEnemy;
	}

	private IEnumerator GenerateEnemyes()
	{
		var delay = new WaitForSeconds(_reapitingRate);

		while (gameObject.activeSelf)
		{
			GetEnemy();
			yield return delay;
		}
	}

	private void FillStartPoints()
	{
		foreach (Transform child in _startPonints)		
			_spawnPointsTransform.Add(child);		
	}

	private Transform GetSpawnPoint()
	{
		return _spawnPointsTransform[Random.Range(0, _spawnPointsTransform.Count)];
	}

	private void RealiseEnemy(Enemy enemy)
	{
		enemy.CameOut -= RealiseEnemy;

		_pool.Release(enemy);
	}
}
