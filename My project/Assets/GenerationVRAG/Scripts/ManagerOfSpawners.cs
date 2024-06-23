using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfSpawners : MonoBehaviour
{
	[SerializeField] GameObject AllPoints;

	[SerializeField] private float _reapitingRate = 2f;

	private List<Spawner> _spawners = new();

	private void Awake()
	{
		FillSpawners();
		StartCoroutine(GetEnemy());
	}

	private void FillSpawners()
	{
		foreach (Spawner spawner in AllPoints.GetComponentsInChildren<Spawner>())
			_spawners.Add(spawner);
	}

	private IEnumerator GetEnemy()
	{
		var delay = new WaitForSeconds(_reapitingRate);

		if (_spawners == null)
		{
			Debug.Log("SpawnPoints Is Empty");
			yield return null;
		}

		while (gameObject.activeSelf)
		{
			int randomIndexOfSpownPoints = Random.Range(0, _spawners.Count);

			_spawners[randomIndexOfSpownPoints].GetEnemy();
			yield return delay;
		}
	}
}
