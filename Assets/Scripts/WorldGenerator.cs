using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
	/// <summary>
	/// Game object where all generated world elements will be placed on
	/// </summary>
	public GameObject ParentGameObject;
	public int SizeX = 20;
	public int SizeY = 20;
	public int MaxObstacles = 40;

	public GameObject Player;
	public Vector3 PlayerStartPosition;

	public int SandDistanceToBorder = 3;
	public int DirtDistanceToBorder = 5;

	private GameObject _sandGround;
	private GameObject _dirtGround;
	private GameObject _grassGround;
	private GameObject _sandBag;
	private IList<GameObject> _obstacles = new List<GameObject> ();
	private float _gridSize = 1.28f;

	private void LoadResources ()
	{
		_sandGround = Resources.Load ("Ground/SandGround", typeof(GameObject)) as GameObject;
		_dirtGround = Resources.Load ("Ground/DirtGround", typeof(GameObject)) as GameObject;
		_grassGround = Resources.Load ("Ground/GrassGround", typeof(GameObject)) as GameObject;
		_sandBag = Resources.Load ("Ground/BrownSandbag", typeof(GameObject)) as GameObject;

		_obstacles.Add (Resources.Load ("Ground/LargeTree", typeof(GameObject)) as GameObject);
		_obstacles.Add (Resources.Load ("Ground/SmallTree", typeof(GameObject)) as GameObject);
	}

	public void Generate ()
	{
		LoadResources ();

		Clear ();

		GenerateCells ();
		GenerateBorder ();
		GenerateObstacles ();
		GeneratePlayer ();
	}

	/// <summary>
	/// Clears all the previous generated world elements
	/// </summary>
	private void Clear ()
	{
		for (var child = ParentGameObject.transform.childCount - 1; child >= 0; child--) {
			var c = ParentGameObject.transform.GetChild (child);

			DestroyImmediate (c.gameObject);
		}
	}

	private void GenerateCells ()
	{
		for (var row = 0; row < SizeY; row++) {
			for (var column = 0; column < SizeX; column++) {
				GenerateCell (row, column);
			}
		}
	}

	private void GenerateCell (int row, int column)
	{
		var point = new Vector3 (column * _gridSize, row * _gridSize, 0);
		var objToInstantiate = _grassGround;
		var absoluteDistanceToBorder = AbsoluteDistanceToBorder (point);

		if (absoluteDistanceToBorder < SandDistanceToBorder * _gridSize) {
			objToInstantiate = _sandGround;
		} else if (absoluteDistanceToBorder < DirtDistanceToBorder * _gridSize) {
			objToInstantiate = _dirtGround;
		}

		var instance = Instantiate (objToInstantiate, point, Quaternion.identity) as GameObject;
		instance.transform.SetParent (ParentGameObject.transform);
	}

	private float AbsoluteDistanceToBorder(Vector2 point) {
		var distanceToLeftBorder = Vector2.Distance (new Vector2 (0, point.y), point);
		var distanceToRightBorder = Vector2.Distance (new Vector2 (SizeX * _gridSize, point.y), point);
		var distanceToTopBorder = Vector2.Distance (new Vector2 (point.x, SizeY * _gridSize), point);
		var distanceToBottomBorder = Vector2.Distance (new Vector2 (point.x, 0), point);

		// I'm pretty sure, it can be done easier, but it's late night currently. :)
		var minXDistance = Mathf.Min (distanceToLeftBorder, distanceToRightBorder);
		var minYDistance = Mathf.Min (distanceToTopBorder, distanceToBottomBorder);

		return Mathf.Min (minXDistance, minYDistance);
	}

	private void GenerateBorder ()
	{
		// Since the bags a only half a tile big, we need twice as much 
		for (var column = 0; column < SizeX * 2; column++) {
			// Position correction. This is extremly sandbag dependend. :)
			var xPos = column * _gridSize / 2f - _gridSize / 2f + _gridSize / 14f;
			var bottomBag = Instantiate (_sandBag, new Vector3 (xPos, -_gridSize / 4f, 0), Quaternion.identity) as GameObject;
			bottomBag.transform.SetParent (ParentGameObject.transform);

			var topBag = Instantiate (_sandBag, new Vector3 (xPos, _gridSize * SizeY - _gridSize / 2f, 0), Quaternion.identity) as GameObject;
			topBag.transform.SetParent (ParentGameObject.transform);
		}

		// -1 > We are within the border of the complete game. Top and Bottom border are 1 bag together, so we the complete grid exepect 1 bag
		for (var row = 0; row < SizeY * 2 - 1; row++) {
			// Position correction
			var yPos = row * _gridSize / 2f - _gridSize / 5f;
			var leftBag = Instantiate (_sandBag, new Vector3 (-_gridSize / 2f, yPos, 0), Quaternion.identity) as GameObject;
			leftBag.transform.SetParent (ParentGameObject.transform);
			leftBag.transform.Rotate (0, 0, 90);

			var rightBag = Instantiate (_sandBag, new Vector3 (_gridSize * SizeX - _gridSize + _gridSize / 4f, yPos, 0), Quaternion.identity) as GameObject;
			rightBag.transform.SetParent (ParentGameObject.transform);
			rightBag.transform.Rotate (0, 0, 90);
		}
	}

	private void GenerateObstacles ()
	{
		for (var i = 0; i < MaxObstacles; i++) {
			var whichObstacleToInstanciate = Random.Range (0, _obstacles.Count);

			// Avoid placing objects to near at the border
			// DON'T calculate the random first and multiply it to avoid placing the obstacles on a grid
			// Obstacles should be placed freely
			// We actually ALLOW overlapping obstacles
			var position = GetObstaclePosition();

			var obstacle = Instantiate (_obstacles [whichObstacleToInstanciate], position, Quaternion.identity) as GameObject;
			obstacle.transform.SetParent (ParentGameObject.transform);
		} 
	}

	private Vector3 GetObstaclePosition ()
	{
		var position = new Vector3 (Random.Range (2 * _gridSize, _gridSize * (SizeX - 2)), Random.Range (2 * _gridSize, _gridSize * (SizeY - 2)), 0);

		return position;
	}

	private void GeneratePlayer () {
		// Dont create a player if it is not assigned or we are in editor mode
		if (Player == null || Application.isEditor && !Application.isPlaying) {
			return;
		}

		var player = Instantiate (Player, PlayerStartPosition, Quaternion.identity) as GameObject;

		TrySetCameraToPlayer (player);
	}

	// Maybe should be in another script?
	private void TrySetCameraToPlayer(GameObject player) {
		var mainCamera = GameObject.FindGameObjectWithTag (CustomTags.MainCamera);

		if (mainCamera == null) {
			return;
		}

		var followScript = mainCamera.GetComponent<CameraFollow> ();

		if (followScript == null) {
			return;
		}

		Debug.Log ("Setting main camera to follow player");
		followScript.FollowWhat = player;
	}
}
