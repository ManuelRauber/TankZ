using UnityEngine;
using System.Collections;

public class PlayerTank : MonoBehaviour {
	public float Speed = 80f;
	public float TurnSpeed = 1.3f;
	public GameObject Cannon;
	public float CannonTurnRate = 30f;
	private float _powerInput;
	private float _turnInput;
	private Vector3 _mousePosition;

	private Rigidbody2D _tankBody;

	public void Awake() 
	{
		_tankBody = GetComponent<Rigidbody2D> ();

		if (Cannon == null) {
			throw new MissingReferenceException("No cannon attached!");
		}
	}

	public void Update() 
	{
		_powerInput = Input.GetAxis ("Vertical");
		_turnInput = Input.GetAxis ("Horizontal");
		_mousePosition = Input.mousePosition;
	}

	public void FixedUpdate()
	{
		MoveTank ();
		CannonMouseFollow ();
	}

	private void MoveTank() 
	{
		_tankBody.AddRelativeForce (new Vector2 (0f, _powerInput * Speed));
		_tankBody.MoveRotation (_tankBody.rotation - _turnInput * TurnSpeed);
	}

	private void CannonMouseFollow() 
	{
		var position = Camera.main.ScreenToWorldPoint (_mousePosition);
		var rotation = Quaternion.LookRotation (position - Cannon.transform.position, Vector3.back);
		var targetRotation = Quaternion.RotateTowards (Cannon.transform.rotation, rotation, CannonTurnRate);
		Cannon.transform.rotation = new Quaternion (0, 0, targetRotation.z, targetRotation.w);
	}


}