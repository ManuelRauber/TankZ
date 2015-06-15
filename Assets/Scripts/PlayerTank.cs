using UnityEngine;
using System.Collections;

public class PlayerTank : MonoBehaviour {
	/// <summary>
	/// The maximum speed the tank can achieve
	/// </summary>
	public float Speed = 80f;

	/// <summary>
	/// Maximum turn speed of the tank
	/// </summary>
	public float TurnSpeed = 1.3f;

	/// <summary>
	/// A game object which represents the barrel. It is used for the mouse follow function
	/// </summary>
	public GameObject Cannon;

	/// <summary>
	/// Maximum turn rate of the cannon
	/// </summary>
	public float CannonTurnRate = 30f;

	/// <summary>
	/// Holds the current "power input" (-1..1). -1 Backwards, +1 forwards (S and W key)
	/// </summary>
	private float _powerInput;

	/// <summary>
	/// Holds the current "turn input" (-1..1). -1 turn left, +1 turn right (A and D key)
	/// </summary>
	private float _turnInput;

	/// <summary>
	/// Holds the current mouse position
	/// </summary>
	private Vector3 _mousePosition;

	/// <summary>
	/// The actual tank body where the force and rotation will be applied
	/// </summary>
	private Rigidbody2D _tankBody;

	public void Awake() 
	{
		// Get the rigidbody
		_tankBody = GetComponent<Rigidbody2D> ();

		// If no cannon is assigned we throw an error. This means using a cannon is mandatory
		if (Cannon == null) {
			throw new MissingReferenceException("No cannon attached!");
		}
	}

	public void Update() 
	{
		// The input of W and S key
		_powerInput = Input.GetAxis ("Vertical");

		// The input of A and D key
		_turnInput = Input.GetAxis ("Horizontal");

		// Current mouse position
		_mousePosition = Input.mousePosition;
	}

	public void FixedUpdate()
	{
		MoveTank ();
		CannonMouseFollow ();
	}

	private void MoveTank() 
	{
		// Add a relative force according to power input and speed settings
		_tankBody.AddRelativeForce (new Vector2 (0f, _powerInput * Speed));

		// rotate the tank according to the turn input and turn speed
		_tankBody.MoveRotation (_tankBody.rotation - _turnInput * TurnSpeed);
	}

	private void CannonMouseFollow() 
	{
		// Translate the mouse position into a point in the world
		var position = Camera.main.ScreenToWorldPoint (_mousePosition);

		// Calculate the rotation to make the cannon look at the mouse position
		var rotation = Quaternion.LookRotation (position - Cannon.transform.position, Vector3.back);

		// Since we don't want to do a instant rotation, calculate the rotation towards the target rotation according to the turn state
		var targetRotation = Quaternion.RotateTowards (Cannon.transform.rotation, rotation, CannonTurnRate);

		// Assign the new rotation to the cannon
		Cannon.transform.rotation = new Quaternion (0, 0, targetRotation.z, targetRotation.w);
	}
}