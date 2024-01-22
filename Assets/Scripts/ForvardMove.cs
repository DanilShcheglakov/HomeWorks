using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForvard : MonoBehaviour
{
	[SerializeField] private bool _isLocalForvard;

	private void Update()
	{
		if (_isLocalForvard)
			transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
		else
			transform.Translate(Vector3.forward * Time.deltaTime);
	}

}
