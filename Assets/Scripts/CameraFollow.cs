using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject FollowWhat;
	public float FollowSpeed = 5.0f;

	public void Update() {
		transform.position = Vector3.Lerp (transform.position, new Vector3(FollowWhat.transform.position.x, FollowWhat.transform.position.y, transform.position.z), Time.deltaTime * FollowSpeed);
	}
}
