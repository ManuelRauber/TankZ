using UnityEngine;
using System.Collections;

// Ugly solution for passing data from one scene to another
// Should be switched to an GameObject with "DontDestroyOnLoad" later on
public static class SharedData {
	public static int Obstacles;
	public static int MapSizeX;
	public static int MapSizeY;
}
