using AudioSystem.Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace AudioSystem.AudioPlayers
{
    public class PlayOnButtonClick : BetterMonoBehaviour
    {
        [SerializeField]
        private AudioEvent clickEvent;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            
            button.onClick.AddListener(PlayAudio);
        }

        private void PlayAudio()
        {
            AudioPlayer.PlayAudio(clickEvent);
        }
    }
}
