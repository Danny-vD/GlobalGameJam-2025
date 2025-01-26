using AudioSystem.Enums;
using UnityEngine;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class PlayOnTriggerEnter : BetterMonoBehaviour
	{
		[SerializeField]
		private AudioEvent audioEvent;

		private void OnTriggerEnter(Collider other)
		{
			PlayAudio();
		}

		private void PlayAudio()
		{
			AudioPlayer.PlayAudio(audioEvent);
		}

	}
}