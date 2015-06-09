using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private Animator _animator;
	private float _nextFireTime;

	public ParticleSystem SmokeSystem;
	public AudioSource AttackSound;
	public GameObject Bullet;
	public GameObject BulletStartPoint;
	public float FirePower = 2.0f;
	public float ReloadTime = 0.4f;

	public void Awake() 
	{
		_animator = GetComponent<Animator> ();

		if (Bullet == null) {
			throw new MissingComponentException("Bullet is missing");
		}

		if (BulletStartPoint == null) {
			throw new MissingComponentException("BulletStartPoint is missing");
		}

		_nextFireTime = Time.time;
	}

	void Update () {
		FireCannon ();
	}

	private void FireCannon() 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			if (Time.time <= _nextFireTime) {
				return;
			}

			_nextFireTime = Time.time + ReloadTime;

			_animator.SetTrigger("FireTrigger");

			var bullet = (GameObject) Instantiate(Bullet, BulletStartPoint.transform.position, BulletStartPoint.transform.rotation);
			var bulletBody = (Rigidbody2D) bullet.GetComponentInChildren(typeof(Rigidbody2D));
			bulletBody.AddForce(bullet.transform.up * FirePower, ForceMode2D.Impulse);

			if (SmokeSystem != null) 
			{
				var smoke = (ParticleSystem) Instantiate(SmokeSystem, SmokeSystem.transform.position, SmokeSystem.transform.rotation);
				smoke.Play();
				Destroy(smoke.gameObject, smoke.startLifetime * 1.1f);
			}

			if (AttackSound != null) 
			{
				var sound = (AudioSource) Instantiate(AttackSound, BulletStartPoint.transform.position, BulletStartPoint.transform.rotation);
				sound.Play();
				Destroy (sound.gameObject, sound.clip.length);
			}
		}
	}
}
