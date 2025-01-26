using AudioSystem.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class PlayOnPointerDown : BetterMonoBehaviour, IPointerDownHandler
	{
		[SerializeField]
		private AudioEvent clickEvent;

		private void PlayAudio()
		{
			AudioPlayer.PlayAudio(clickEvent);
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			PlayAudio();
		}
	}
}