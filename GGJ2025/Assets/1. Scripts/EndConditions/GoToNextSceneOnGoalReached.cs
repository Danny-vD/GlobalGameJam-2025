using GameplayEvents;
using UnityEngine.SceneManagement;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.ObserverPattern.Constants;

namespace EndConditions
{
	public class GoToNextSceneOnGoalReached : BetterMonoBehaviour
	{
		private void OnEnable()
		{
			EventManager.AddListener<PlayerSucceededEvent>(LoadNextScene, Priority.LOWEST); 
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<PlayerSucceededEvent>(LoadNextScene);
		}

		private void LoadNextScene()
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
			buildIndex %= SceneManager.sceneCountInBuildSettings;
			
			SceneManager.LoadScene(buildIndex);
		}
	}
}