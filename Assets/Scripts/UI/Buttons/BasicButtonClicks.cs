using UnityEngine;
using System.Collections;

public class BasicButtonClicks : MonoBehaviour {

	public void GitHubButtonClicked() {
		Application.OpenURL ("https://github.com/ManuelRauber/TankZ");
	}

	public void LoadLevel(string levelName) {
		Application.LoadLevel (levelName);
	}
}
