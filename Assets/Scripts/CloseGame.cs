using UnityEngine;
using System.Collections;

public class CloseGame : MonoBehaviour {

	public void Start() {
		if (Application.isWebPlayer || Application.platform == RuntimePlatform.OSXEditor) {
			gameObject.SetActive(false);
		}
	}

	public void OnClick() {
		Application.Quit ();
	}
}
