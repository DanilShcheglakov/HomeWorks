using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
	[SerializeField] private float _scaleGrowSpeed;

	private void Update()
	{
		transform.localScale += new Vector3(1, 1, 1) * _scaleGrowSpeed * Time.deltaTime;
	}
}
