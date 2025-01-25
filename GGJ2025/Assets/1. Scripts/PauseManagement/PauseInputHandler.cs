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
			if (!ReferenceEquals(pauseInput, null))
			{
				pauseInput.action.performed += Pause;
			}

			if (!ReferenceEquals(resumeInput, null))
			{
				resumeInput.action.performed += Resume;
			}

			if (!ReferenceEquals(togglePauseInput, null))
			{
				togglePauseInput.action.performed += TogglePause;
			}
		}

		private void OnDisable()
		{
			if (!ReferenceEquals(pauseInput, null))
			{
				pauseInput.action.performed -= Pause;
			}

			if (!ReferenceEquals(resumeInput, null))
			{
				resumeInput.action.performed -= Resume;
			}

			if (!ReferenceEquals(togglePauseInput, null))
			{
				togglePauseInput.action.performed -= TogglePause;
			}
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