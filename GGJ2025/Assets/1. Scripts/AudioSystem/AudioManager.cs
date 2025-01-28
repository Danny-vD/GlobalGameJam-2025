using System.Collections.Generic;
using System.Linq;
using AudioSystem.Enums;
using AudioSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;
using VDFramework.Extensions;
using VDFramework.Singleton;
using VDFramework.UnityExtensions;

namespace AudioSystem
{
	public class AudioManager : Singleton<AudioManager>
	{
		[SerializeField]
		private AudioClipData audioClipData;

		[SerializeField]
		private AudioMixerGroup SfxGroup;

		[SerializeField]
		private AudioMixerGroup MusicGroup;

		[Space]
		[SerializeField, Tooltip("If enabled, and possible, the same audioclip will not be played multiple times after eachother")]
		private bool preventPlayingSameSoundInRow = true;

		[Space]
		[SerializeField, Tooltip("How many audiosources are reserved for playing looping audio")]
		private byte maximumLoopingAudioSourcesCount = 2;

		[SerializeField, Tooltip("How many audiosources are available for playing non-looping audio")]
		private byte maximumAudioSourcesCount = 5;

		private AudioEvent lastPlayedEvent = (AudioEvent)(-1);
		private int lastPlayedIndex = -1;

		private List<AudioSource> loopingAudioSources;
		private List<AudioSource> audioSources;

		protected override void Awake()
		{
			base.Awake();

			loopingAudioSources = new List<AudioSource>(maximumLoopingAudioSourcesCount);
			audioSources        = new List<AudioSource>(maximumAudioSourcesCount);
		}

		private void Start()
		{
			if (!transform.parent)
			{
				DontDestroyOnLoad(true);
			}
		}

		public AudioClip GetAudioClip(AudioEvent audioEvent)
		{
			AudioClip[] audioClips = audioClipData.GetAudioClips(audioEvent);

			AudioClip clipToPlay = null;

			if (preventPlayingSameSoundInRow && audioEvent == lastPlayedEvent && audioClips.Length > 1)
			{
				audioClips.GetRandomElement(out lastPlayedIndex, lastPlayedIndex);
				lastPlayedEvent = audioEvent;
			}
			else
			{
				clipToPlay = audioClips.GetRandomElement();
			}

			return clipToPlay;
		}

		public AudioSource GetAudioSource(bool shouldLoop)
		{
			AudioSource audioSource = shouldLoop ? loopingAudioSources.GetFirstNotPlaying() : audioSources.GetFirstNotPlaying();

			if (audioSource == null)
			{
				if (shouldLoop)
				{
					if (loopingAudioSources.Count == maximumLoopingAudioSourcesCount)
					{
						// If we reached the limit of sounds we can play simulaneously, use the oldest audiosource
						audioSource = GetLongestPlayingAudioSource(loopingAudioSources);
						audioSource.Stop();
					}
					else
					{
						audioSource = gameObject.AddComponent<AudioSource>();

						audioSource.loop                  = true;
						audioSource.outputAudioMixerGroup = MusicGroup;

						loopingAudioSources.Add(audioSource);
					}
				}
				else
				{
					if (audioSources.Count == maximumAudioSourcesCount)
					{
						// If we reached the limit of sounds we can play simulaneously, use the oldest audiosource
						audioSource = GetLongestPlayingAudioSource(audioSources);
						audioSource.Stop();
					}
					else
					{
						audioSource = gameObject.AddComponent<AudioSource>();

						audioSource.outputAudioMixerGroup = SfxGroup;

						audioSources.Add(audioSource);
					}
				}
			}

			return audioSource;
		}

		public AudioSource GetPlayingAudioSource(AudioEvent audioEvent)
		{
			AudioClip clip = Instance.GetAudioClip(audioEvent);

			AudioSource audioSource = GetLongestPlayingAudioSource(loopingAudioSources, clip);

			if (audioSource == null)
			{
				audioSource = GetLongestPlayingAudioSource(audioSources, clip);
			}

			return audioSource;
		}

		private static AudioSource GetLongestPlayingAudioSource(IReadOnlyList<AudioSource> collection)
		{
			AudioSource longestPlayingSource = collection[0];
			float furthestTime = longestPlayingSource.time;

			for (int i = 1; i < collection.Count; i++)
			{
				AudioSource source = collection[i];

				if (source.time > furthestTime)
				{
					furthestTime         = source.time;
					longestPlayingSource = source;
				}
			}

			return longestPlayingSource;
		}
		
		private static AudioSource GetLongestPlayingAudioSource(IReadOnlyList<AudioSource> collection, AudioClip clip)
		{
			AudioSource longestPlayingSource = collection.FirstOrDefault(source => source.clip == clip);

			if (longestPlayingSource == null) // Will only be null if no source is playing that clip, in which case it's pointless to search further
			{
				return null;
			}	
			
			float furthestTime = longestPlayingSource.time;

			for (int i = 1; i < collection.Count; i++)
			{
				AudioSource source = collection[i];

				if (source.clip != clip)
				{
					continue;
				}

				if (source.time > furthestTime)
				{
					furthestTime         = source.time;
					longestPlayingSource = source;
				}
			}

			return longestPlayingSource;
		}
	}
}