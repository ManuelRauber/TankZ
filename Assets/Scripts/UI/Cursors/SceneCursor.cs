using UnityEngine;
using System.Collections;

public class SceneCursor : MonoBehaviour {
	public Sprite cursorSprite;
	public CursorMode cursorMode = CursorMode.Auto;
	public float HotSpotPivotX = 0;
	public float HotSpotPivotY = 0;

	public void Start () {
		if (cursorSprite == null) {
			return;
		}

		var texture = SpriteToTexture ();
		var hotSpot = new Vector2 (texture.width * HotSpotPivotX, texture.height * HotSpotPivotY); 
		Cursor.SetCursor(texture, hotSpot, cursorMode);
	}

	private Texture2D SpriteToTexture() {
		var croppedTexture = new Texture2D((int)cursorSprite.rect.width, (int)cursorSprite.rect.height);
		
		var pixels = cursorSprite.texture.GetPixels(  (int)cursorSprite.textureRect.x, 
		                                            (int)cursorSprite.textureRect.y, 
		                                            (int)cursorSprite.textureRect.width, 
		                                            (int)cursorSprite.textureRect.height );
		
		croppedTexture.SetPixels( pixels );
		croppedTexture.Apply();

		return croppedTexture;
	}
}