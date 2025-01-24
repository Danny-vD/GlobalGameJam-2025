using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace PauseManagement
{
	public class PauseInputHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private InputActionReference pauseInput;

		[SerializeField]
		private InputActionReference resumeInput;

		[SerializeField]
		private InputActionReference togglePauseInput;
	}
}