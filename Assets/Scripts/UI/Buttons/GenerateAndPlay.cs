using UnityEngine;
using System.Collections;

public class GenerateAndPlay : MonoBehaviour {
	public int MapSizeX = 10;
	public int MapSizeY = 10;
	public int Obstacles = 10;

	public void UpdateMapSizeX(float value) {
		MapSizeX = Mathf.RoundToInt (value);
	}

	public void UpdateMapSizeY(float value) {
		MapSizeY = Mathf.RoundToInt (value);
	}

	public void UpdateObstacles(float value) {
		Obstacles = Mathf.RoundToInt (value);
	}

	public void Clicked() {
		SharedData.Obstacles = Obstacles;
		SharedData.MapSizeX = MapSizeX;
		SharedData.MapSizeY = MapSizeY;

		Application.LoadLevel ("OperationWahooka");
	}
}
