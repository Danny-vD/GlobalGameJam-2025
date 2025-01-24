using UnityEngine;
using UnityEngine.SceneManagement;
using VDFramework;

namespace UI
{
    public class UIFunctionality : BetterMonoBehaviour
    {
        public void Quit()
        {
#if !UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
			UnityEngine.Application.Quit();
#endif
		}

		public void LoadScene(int buildIndex)
		{
			SceneManager.LoadScene(buildIndex);
		}

		public void LoadNextScene()
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
			buildIndex %= SceneManager.sceneCountInBuildSettings;
			
			LoadScene(buildIndex);
		}

		public void LoadPreviousScene()
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex - 1;

			if (buildIndex < 0)
			{
				buildIndex = SceneManager.sceneCountInBuildSettings - 1;
			}
			
			LoadScene(buildIndex);
		}
    }
}
