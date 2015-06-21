using UnityEngine;
using System.Collections;

// Creating a game object per trail is not the best idea...
public class TankTrail : MonoBehaviour {
	public GameObject SingleMarkVisual;
	public float MarkDistance = 0.18f;
	public float MarkLifeTime = 5f;

	private float _absoluteMovingDistance;
	private Vector2 _lastMovePosition;

	public void Awake() {
		_lastMovePosition = transform.position;
	}

	public void Update () {
		var position = transform.position;

		var distance = Vector2.Distance (position, _lastMovePosition);

		_lastMovePosition = position;

		_absoluteMovingDistance += distance;

		if (_absoluteMovingDistance > MarkDistance) {
			var trail = Instantiate(SingleMarkVisual, position, transform.rotation);

			_absoluteMovingDistance -= MarkDistance;

			Destroy(trail, MarkLifeTime);
		}
	}
}