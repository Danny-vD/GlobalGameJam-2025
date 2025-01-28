using AudioSystem.Enums;
using UnityEngine;

namespace AudioSystem
{
	public static class AudioPlayer
	{
		public static void PlayAudio(AudioEvent audioEvent, bool loop = false)
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}
			
			AudioSource source = AudioManager.Instance.GetAudioSource(loop);

			AudioClip clip = AudioManager.Instance.GetAudioClip(audioEvent);
			source.clip = clip;
			
			source.Play();
		}

		public static void StopAudio(AudioEvent audioEvent)
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}
			
			AudioSource source = AudioManager.Instance.GetPlayingAudioSource(audioEvent);

			if (source != null)
			{
				source.Stop();
			}
		}
	}
}