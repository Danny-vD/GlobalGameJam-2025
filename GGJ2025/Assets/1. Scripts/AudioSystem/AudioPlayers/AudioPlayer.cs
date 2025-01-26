using AudioSystem.Enums;
using UnityEngine;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class AudioPlayerComponent : BetterMonoBehaviour
	{
		[SerializeField]
		private AudioEvent audioEvent;

		public void PlayAudio()
		{
			AudioPlayer.PlayAudio(audioEvent);
		}
		
		public void PlayAudio(AudioEvent audioEventParameter)
		{
			AudioPlayer.PlayAudio(audioEventParameter);
		}
	}
}