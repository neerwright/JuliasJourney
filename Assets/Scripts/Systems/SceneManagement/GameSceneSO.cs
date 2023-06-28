using UnityEngine;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations, Menus, Managers)
/// </summary>
namespace SceneManagement
{
	[CreateAssetMenu(fileName = "GameScene", menuName = "GameSceneSO")]
	public class GameSceneSO : ScriptableObject
	{
		public GameSceneType sceneType;
		public string sceneName; //Used at runtime to load the scene from the right AssetBundle

		/// <summary>
		/// Used by the SceneSelector tool to discern what type of scene it needs to load
		/// </summary>
		public enum GameSceneType
		{
			//Playable scenes
			Location, //SceneSelector tool will also load PersistentManagers and Gameplay
			Menu, //SceneSelector tool will also load Gameplay

			//Special scenes
			Initialisation,
			PersistentManagers,
			Gameplay
		}
	}
}