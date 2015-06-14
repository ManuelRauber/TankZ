using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {
	public float BulletLifeTime = 3.0f;
	private float _startTime;

	public GameObject Explosion;

	public void Awake() {
		_startTime = Time.time;
	}

	public void Update() {
		if (Time.time - _startTime > BulletLifeTime) {
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == CustomTags.Wood) {
			Explode();
			Destroy (gameObject);
		}
	}

	private void Explode() {
		Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
	}
}
