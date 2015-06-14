using UnityEngine;
using System.Collections;

public class CreateWorldOnAwake : MonoBehaviour {
	public void Awake() {
		var worldGenerator = GetComponent (typeof(WorldGenerator)) as WorldGenerator;
		worldGenerator.MaxObstacles = SharedData.Obstacles;
		worldGenerator.SizeX = SharedData.MapSizeX;
		worldGenerator.SizeY = SharedData.MapSizeY;
		worldGenerator.Generate ();
	}
}
