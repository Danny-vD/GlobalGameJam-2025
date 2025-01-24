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

		private void OnEnable()
		{
			pauseInput.action.performed       += Pause;
			resumeInput.action.performed      += Resume;
			togglePauseInput.action.performed += TogglePause;
		}

		private void OnDisable()
		{
			pauseInput.action.performed       -= Pause;
			resumeInput.action.performed      -= Resume;
			togglePauseInput.action.performed -= TogglePause;
		}

		public void Pause(InputAction.CallbackContext callbackContext)
		{
			PauseManager.Pause();
		}

		public void Resume(InputAction.CallbackContext callbackContext)
		{
			PauseManager.Resume();
		}

		public void TogglePause(InputAction.CallbackContext callbackContext)
		{
			PauseManager.TogglePause();
		}
	}
}