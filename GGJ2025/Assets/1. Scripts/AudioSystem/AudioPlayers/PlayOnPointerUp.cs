using AudioSystem.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class PlayOnPointerUp : BetterMonoBehaviour, IPointerUpHandler
	{
		[SerializeField]
		private AudioEvent clickEvent;

		private void PlayAudio()
		{
			AudioPlayer.PlayAudio(clickEvent);
		}
		
        public void OnPointerUp(PointerEventData eventData)
        {
			PlayAudio();
		}
    }
}