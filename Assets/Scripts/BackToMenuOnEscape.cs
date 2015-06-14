using UnityEngine;
using System.Collections;

public class BackToMenuOnEscape : MonoBehaviour {
	public void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("GenerateNewMapScene");
		}
	}
}
