using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
	[SerializeField] private float _scaleGrowSpeed;

	private void Update()
	{
		transform.localScale += Vector3.one * _scaleGrowSpeed * Time.deltaTime;
	}
}
