using UnityEngine;
using VDFramework.Singleton;

namespace PauseManagement
{
	public class PauseManager : Singleton<PauseManager>
	{
		public static bool IsPaused { get; private set; }

		private static float previousTimeScale = 1;
		
		[SerializeField]
		private bool overridePausedState = false;

		[SerializeField]
		private bool startPaused = false;

		private void Start()
		{
			if (overridePausedState)
			{
				if (IsPaused && !startPaused)
				{
					Resume();
				}
				else if (!IsPaused && startPaused)
				{
					Pause();
				}
			}

			if (IsPaused && Time.timeScale > 0) // Security in case something else messed with the timescale
			{
				Time.timeScale = 0;
			}
		}

		public static void Pause()
		{
			if (IsPaused)
			{
				return;
			}

			previousTimeScale = Time.timeScale;
			
			IsPaused       = true;
			Time.timeScale = 0;
		}

		public static void Resume()
		{
			if (!IsPaused)
			{
				return;
			}
			
			IsPaused       = false;
			Time.timeScale = previousTimeScale;
		}

		public static void SetPause(bool paused)
		{
			if (paused)
			{
				Pause();
			}
			else
			{
				Resume();
			}
		}

		public static void TogglePause()
		{
			SetPause(!IsPaused);
		}
	}
}