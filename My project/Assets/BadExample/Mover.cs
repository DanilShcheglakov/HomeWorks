using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	private Transform _allTargets;
	private Transform[] _targets;

	private float _speed;
	private int _targetIndex = 0;

	private void Start()
	{
		_targets = new Transform[_allTargets.childCount];

		for (int i = 0; i < _targets.Length; i++)		
			_targets[i] = _allTargets.GetChild(i).transform;		
	}

	private void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, _targets[_targetIndex].position, _speed * Time.fixedDeltaTime);

		if (transform.position == _targets[_targetIndex].position)
			SetNextTarget();
	}

	private void SetNextTarget()
	{
		_targetIndex = (_targetIndex++) % _targets.Length;

		transform.forward = _targets[_targetIndex].position - transform.position;
	}
}