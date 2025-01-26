using AudioSystem.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;

namespace AudioSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Create AudioClipData", fileName = "AudioClipData", order = 0)]
	public class AudioClipData : ScriptableObject
	{
		[SerializeField]
		private SerializableEnumDictionary<AudioEvent, AudioClip[]> audioClips;

		public AudioClip[] this[AudioEvent audioEvent] => audioClips[audioEvent];

		public AudioClip[] GetAudioClips(AudioEvent audioEvent)
		{
			return audioClips[audioEvent];
		}
	}
}