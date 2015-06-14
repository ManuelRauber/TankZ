using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour {
	/// <summary>
	/// Game object where all generated world elements will be placed on
	/// </summary>
	public GameObject ParentGameObject;

	public int SizeX = 20;
	public int SizeY = 20;

	private GameObject _sandGround;
	private GameObject _dirtGround;
	private GameObject _grassGround;
	private GameObject _sandBag;

	private float _gridSize = 1.28f;

	private void LoadResources() {
		_sandGround = Resources.Load ("Ground/SandGround", typeof(GameObject)) as GameObject;
		_dirtGround = Resources.Load ("Ground/DirtGround", typeof(GameObject)) as GameObject;
		_grassGround = Resources.Load ("Ground/GrassGround", typeof(GameObject)) as GameObject;
		_sandBag = Resources.Load ("Ground/BrownSandbag", typeof(GameObject)) as GameObject;
	}

	public void Generate() {
		LoadResources ();

		Clear ();

		GenerateCells ();
		GenerateBorder ();
	}

	/// <summary>
	/// Clears all the previous generated world elements
	/// </summary>
	private void Clear() {
		for (var child = ParentGameObject.transform.childCount - 1; child >= 0; child--) {
			var c = ParentGameObject.transform.GetChild(child);

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
		var instance = Instantiate(_sandGround, new Vector3(column * _gridSize, row * _gridSize, 0), Quaternion.identity) as GameObject;
		instance.transform.SetParent (ParentGameObject.transform);
	}

	private void GenerateBorder ()
	{
		// Since the bags a only half a tile big, we need twice as much 
		for (var column = 0; column < SizeX * 2; column++) {
			// Position correction. This is extremly sandbag dependend. :)
			var xPos = column * _gridSize / 2f - _gridSize / 2f + _gridSize / 14f;
			var bottomBag = Instantiate(_sandBag, new Vector3(xPos, -_gridSize/4f, 0), Quaternion.identity) as GameObject;
			bottomBag.transform.SetParent(ParentGameObject.transform);

			var topBag = Instantiate(_sandBag, new Vector3(xPos, _gridSize * SizeY - _gridSize/2f, 0), Quaternion.identity) as GameObject;
			topBag.transform.SetParent(ParentGameObject.transform);
		}

		// -1 > We are within the border of the complete game. Top and Bottom border are 1 bag together, so we the complete grid exepect 1 bag
		for (var row = 0; row < SizeY * 2 - 1; row++) {
			// Position correction
			var yPos = row * _gridSize / 2f - _gridSize / 5f;
			var leftBag = Instantiate(_sandBag, new Vector3(-_gridSize / 2f, yPos, 0), Quaternion.identity) as GameObject;
			leftBag.transform.SetParent(ParentGameObject.transform);
			leftBag.transform.Rotate(0, 0, 90);

			var rightBag = Instantiate(_sandBag, new Vector3(_gridSize * SizeX - _gridSize + _gridSize /4f, yPos, 0), Quaternion.identity) as GameObject;
			rightBag.transform.SetParent(ParentGameObject.transform);
			rightBag.transform.Rotate(0, 0, 90);
		}
	}
}
