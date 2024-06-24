using System;
using UnityEngine;

public class ProtectedArea : MonoBehaviour
{
	public event Action StrangerCome;
	public event Action StrangerExit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<Player>(out _))
			StrangerCome?.Invoke();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent<Player>(out _))
			StrangerExit?.Invoke();
	}
}
