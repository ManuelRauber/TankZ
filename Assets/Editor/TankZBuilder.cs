using UnityEditor;

public class TankZBuilder {
	private const string MenuPrefix = "TankZ/";

	// Add a menu item, so we can use this code in the editor, too
	[MenuItem(MenuPrefix + "Build/WebGL")]
	public static void WebGlBuild() {
		// Scenes which will be built
		string[] scenes = {
			"Assets/Scenes/MainMenuScene.unity",
			"Assets/Scenes/GenerateNewMapScene.unity",
			"Assets/Scenes/OperationWahooka.unity"
		};

		// Add WebGL to the build pipeline
		BuildPipeline.BuildPlayer (scenes, 
		                          "Build/WebGL",
		                          BuildTarget.WebGL,
		                          BuildOptions.None);
	}
}
