using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class StatBar : MonoBehaviour
    {
        private const string iconResourcesPath = "Icons";
        [SerializeField] private Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetIcon(string iconName)
        {
            var sprite = Resources.Load<Sprite>($"{iconResourcesPath}/{iconName}");
            _icon.sprite = sprite;
        }

        public class Factory : PlaceholderFactory<StatBar>
        {
        }
    }
}
