using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
	/// <summary>
	/// The animator used to controll the animations
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// The time when fire can be triggered again
	/// </summary>
	private float _nextFireTime;

	/// <summary>
	/// The particle system used for creating the smoke when shooting
	/// </summary>
	public ParticleSystem SmokeSystem;

	/// <summary>
	/// A sound which is played when shooting
	/// </summary>
	public AudioSource AttackSound;

	/// <summary>
	/// The bullet which is instantiated when shooting
	/// </summary>
	public GameObject Bullet;

	/// <summary>
	/// Where the bullet will be instantiated
	/// </summary>
	public GameObject BulletStartPoint;

	/// <summary>
	/// Describes the force which is added when shooting to the bullet
	/// </summary>
	public float FirePower = 2.0f;

	/// <summary>
	/// The amount of time to wait before another fire can be triggered
	/// </summary>
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
		// Detect, if the player presses the left mouse button
		if (Input.GetMouseButton(0)) 
		{
			// If the tank is not finished with reloading, skip the fire trigger
			if (Time.time <= _nextFireTime) {
				return;
			}

			// Set the next fire time 
			_nextFireTime = Time.time + ReloadTime;

			// Play the animation
			_animator.SetTrigger("FireTrigger");

			// Instantiate the button and add the force to it
			var bullet = (GameObject) Instantiate(Bullet, BulletStartPoint.transform.position, BulletStartPoint.transform.rotation);
			var bulletBody = (Rigidbody2D) bullet.GetComponentInChildren(typeof(Rigidbody2D));
			bulletBody.AddForce(bullet.transform.up * FirePower, ForceMode2D.Impulse);

			// If we have a smoke system: Play it!
			if (SmokeSystem != null) 
			{
				var smoke = (ParticleSystem) Instantiate(SmokeSystem, SmokeSystem.transform.position, SmokeSystem.transform.rotation);
				smoke.Play();
				Destroy(smoke.gameObject, smoke.startLifetime * 1.1f);
			}

			// If we have a sound: Play it!
			if (AttackSound != null) 
			{
				var sound = (AudioSource) Instantiate(AttackSound, BulletStartPoint.transform.position, BulletStartPoint.transform.rotation);
				sound.Play();
				Destroy (sound.gameObject, sound.clip.length);
			}
		}
	}
}
