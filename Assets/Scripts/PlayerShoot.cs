using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private Animator _animator;
	public ParticleSystem SmokeSystem;

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
				SmokeSystem.Play();
			}
		}
	}
}
