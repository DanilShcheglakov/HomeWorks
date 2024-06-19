using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class ExplosingCube : MonoBehaviour
{
	[Header("Copies settings")]
	[SerializeField] private AllMaterialsForCube _allMaterials;
	[SerializeField] private ExplosingCube _cubePrefab;
	[SerializeField, Min(0)] private int _minCubesCopies;
	[SerializeField] private int _maxCubesCopies;

	[Space(10)]
	[Header("Explosion settings")]

	[SerializeField] int _divisonChance;
	[SerializeField] private float _explosionForce;
	[SerializeField] private float _explosionRadius;

	private Renderer _renderer;

	private void OnValidate()
	{
		if (_maxCubesCopies<=_minCubesCopies)		
			_maxCubesCopies = _minCubesCopies + 1;		
	}

	private void Start()
	{
		_renderer = GetComponent<Renderer>();

		_renderer.material = _allMaterials.GetRandomMaterial();
	}

	private void OnMouseDown()
	{
		Explode();
		Destroy(gameObject);
	}

	private void Explode()
	{
		int explodeChance = Random.Range(0, 100);
		int shards = Random.Range(_minCubesCopies, _maxCubesCopies);

		if (explodeChance>_divisonChance)		
			return;

		for (int i = 0; i < shards; i++)		
			Instantiate(_cubePrefab, transform.position, Quaternion.identity).SetNewState(transform, _explosionForce,_explosionRadius);

    }	

	private void SetNewState(Transform currentTransform, float explosionForce, float explosionRadius)
	{
		transform.localScale = currentTransform.localScale/2;
		_divisonChance /= 2;

		GetComponent<Rigidbody>().AddExplosionForce(explosionForce, currentTransform.position, explosionRadius);
	}
}
