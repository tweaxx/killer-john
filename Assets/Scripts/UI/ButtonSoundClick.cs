using UnityEngine;
using UnityEngine.UI;

namespace TweaxxGames.AnimeAdventure
{
    public class ButtonSoundClick : MonoBehaviour
    {
        public SoundType Type;

        private void Awake()
        {
            var btn = GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => { SoundManager.Instance.PlaySound(Type); });
        }
    }
}
