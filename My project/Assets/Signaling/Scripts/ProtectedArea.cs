using System;
using UnityEngine;

public class ProtectedArea : MonoBehaviour
{
	public event Action<bool> StrangerCome;
	public event Action<bool> StrangerExit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<Player>(out Player _))
			StrangerCome?.Invoke(true);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent<Player>(out Player _))
			StrangerExit?.Invoke(false);
	}
}
