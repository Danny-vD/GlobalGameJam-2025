using System;
using AudioSystem.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using UnityEngine.Audio;
using VDFramework.Singleton;

namespace AudioSystem
{
	public class AudioVolumeManager : Singleton<AudioVolumeManager>
	{
		[SerializeField]
		private AudioMixer audioMixer;

		[SerializeField]
		private SerializableEnumDictionary<AudioBus, string> volumeParameters;

		public float GetVolume(AudioBus bus)
		{
			string volumeParameter = volumeParameters[bus];

			if (audioMixer.GetFloat(volumeParameter, out float volume))
			{
				return volume;
			}

			throw new Exception("No valid parameter");
		}

		public void SetVolume(AudioBus bus, float volume)
		{
			string volumeParameter = volumeParameters[bus];

			if (!audioMixer.SetFloat(volumeParameter, volume))
			{
				throw new Exception("No valid parameter");
			}
		}
	}
}