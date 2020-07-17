using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class BuffBar : Bar
    {
        [Inject]
        public void Init(string text, string icon)
        {
            _text = transform.Find("Text").GetComponent<Text>();
            _icon = transform.Find("Icon").GetComponent<Image>();
            SetIcon(icon);
            SetText(text);
        }

        /// <summary>
        /// First argument - title
        /// Second argument - icon name
        /// </summary>
        public class Factory : PlaceholderFactory<string, string, BuffBar>
        {
        }
    }
}
