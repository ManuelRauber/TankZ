using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {
	public float DestroyTime;

	void Awake() {
		Destroy (gameObject, DestroyTime);
	}
}
