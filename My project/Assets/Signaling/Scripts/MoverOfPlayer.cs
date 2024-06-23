using UnityEngine;

public class MoverOfPlayer : MonoBehaviour
{
	private const string Vertical = "Vertical";
	private const string Horizontal = "Horizontal";

	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;

	private Vector3 _direction;
	private Vector3 _rotation;

	private void Update()
	{
		ReadInput();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void ReadInput()
	{
		_direction = Input.GetAxis(Vertical) * Vector3.forward;

		_rotation = Input.GetAxis(Horizontal) * Vector3.up;
	}

	private void Move()
	{
		transform.Translate(_direction * _moveSpeed * Time.fixedDeltaTime);
		transform.Rotate(_rotation * _rotationSpeed * Time.fixedDeltaTime);
	}
}


