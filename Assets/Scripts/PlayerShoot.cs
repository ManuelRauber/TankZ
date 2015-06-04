using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private Animator _animator;
	public ParticleSystem SmokeSystem;
	public AudioSource AttackSound;

	public void Awake() 
	{
		_animator = GetComponent<Animator> ();
	}

	void Update () {
		FireCannon ();
	}

	private void FireCannon() 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			_animator.SetTrigger("FireTrigger");

			if (SmokeSystem != null) 
			{
				var smoke = (ParticleSystem) Instantiate(SmokeSystem, SmokeSystem.transform.position, SmokeSystem.transform.rotation);
				smoke.Play();
				Destroy(smoke.gameObject, smoke.startLifetime * 1.1f);
			}

			if (AttackSound != null) 
			{
				AttackSound.Play();
			}
		}
	}
}
