using UnityEngine;
using VDFramework;

namespace UI
{
    public class UIFunctionality : BetterMonoBehaviour
    {
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
			return;
#endif
			Application.Quit();
		}
    }
}
