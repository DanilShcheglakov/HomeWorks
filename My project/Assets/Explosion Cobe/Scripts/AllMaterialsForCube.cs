using System.Collections.Generic;
using UnityEngine;

public class AllMaterialsForCube : MonoBehaviour
{
	[SerializeField] private List<Material> _materials;	

	public Material GetRandomMaterial()
	{
		return _materials[Random.Range(0, _materials.Count)];
	}
}
