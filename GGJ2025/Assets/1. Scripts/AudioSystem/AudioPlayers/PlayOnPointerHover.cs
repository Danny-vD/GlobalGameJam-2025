using AudioSystem.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
	public class PlayOnPointerHover : BetterMonoBehaviour, IPointerEnterHandler
	{
		[SerializeField]
		private AudioEvent clickEvent;

		private void PlayAudio()
		{
			AudioPlayer.PlayAudio(clickEvent);
		}

        public void OnPointerEnter(PointerEventData eventData)
        {
			PlayAudio();
		}
    }
}