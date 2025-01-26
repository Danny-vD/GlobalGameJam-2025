using AudioSystem.Enums;
using UnityEngine;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class PlayOnEnable : BetterMonoBehaviour
	{
		[SerializeField]
		private AudioEvent audioToPlay;

		[SerializeField]
		private bool loop = false;

		private void OnEnable()
		{
			AudioPlayer.PlayAudio(audioToPlay, loop);
		}

		private void OnDisable()
		{
			AudioPlayer.StopAudio(audioToPlay);
		}
	}
}