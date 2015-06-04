using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {
	public float BulletLifeTime = 3.0f;
	private float _startTime;

	public void Awake() {
		_startTime = Time.time;
	}

	public void Update() {
		if (Time.time - _startTime > BulletLifeTime) {
			Destroy(gameObject);
		}
	}
}
