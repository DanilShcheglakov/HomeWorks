using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Cube _prefab;
	[Header("Field for spawn")]
	[SerializeField] private Transform _maxXPosition;
	[SerializeField] private Transform _minXPosition;
	[Space(5)]
	[SerializeField] private Transform _maxZPosition;
	[SerializeField] private Transform _minZPosition;
	[SerializeField] private Transform _yPosition;
	[Space(5)]
	[SerializeField] private int _poolCapacity = 10;
	[SerializeField] private int _poolMaxSize = 10;

	[SerializeField] private float _reapitingRate = 1f;

	private float _maxXPoint;
	private float _minXPoint;
	private float _maxZPoint;
	private float _minZPoint;

	private float _yPoint;

	private ObjectPool<Cube> _pool;

	private void OnValidate()
	{
		if (_maxXPosition.position.x <= _minXPosition.position.x)
			_maxXPosition.position = new Vector3(_minXPosition.position.x + 1f, _minXPosition.position.y, _minXPosition.position.z);

		if (_maxXPosition.position.y <= _minXPosition.position.y)
			_maxXPosition.position = new Vector3(_minXPosition.position.x, _minXPosition.position.y + 1f, _minXPosition.position.z);
	}

	private void Awake()
	{
		_maxXPoint = _maxXPosition.position.x;
		_minXPoint = _minXPosition.position.x;

		_maxZPoint = _maxZPosition.position.z;
		_minZPoint = _minZPosition.position.z;

		_yPoint = _yPosition.position.y;

		_pool = new ObjectPool<Cube>(
			createFunc: () => Instantiate(_prefab),
			actionOnGet: (cube) => ActionOnGet(cube),
			actionOnRelease: (cube) => cube.gameObject.SetActive(false),
			actionOnDestroy: (cube) => Destroy(cube.gameObject),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize);
	}

	void Start()
	{
		InvokeRepeating(nameof(GetCube), 0.0f, _reapitingRate);
	}

	private void ActionOnGet(Cube cube)
	{
		cube.SetDefaulSettings(GetRandomPosition());
	}

	private void GetCube()
	{
		if (_pool == null)
			return;

		Cube newCube = _pool.Get();

		newCube.OnDied += RealiseCube;
	}

	private void RealiseCube(Cube cube)
	{
		cube.OnDied -= RealiseCube;

		_pool.Release(cube);
	}

	private Vector3 GetRandomPosition()
	{
		return new Vector3(Random.Range(_minXPoint, _maxXPoint), _yPoint, Random.Range(_minZPoint, _maxZPoint));
	}
}
