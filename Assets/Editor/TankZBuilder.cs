using UnityEditor;

public class TankZBuilder {
	private const string MenuPrefix = "TankZ/";

	[MenuItem(MenuPrefix + "Build/WebGL")]
	public static void WebGlBuild() {
		string[] scenes = {
			"Assets/Scenes/MainMenuScene.unity",
			"Assets/Scenes/GenerateNewMapScene.unity",
			"Assets/Scenes/OperationWahooka.unity"
		};

		BuildPipeline.BuildPlayer (scenes, 
		                          "Build/WebGL",
		                          BuildTarget.WebGL,
		                          BuildOptions.None);
	}
}
