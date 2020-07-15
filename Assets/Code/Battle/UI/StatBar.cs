using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class StatBar : MonoBehaviour
    {
        private const string iconResourcesPath = "Icons";
        private Text _text;
        private Image _icon;

        public int StatId { get; private set; }

        [Inject]
        public void Init(int statId)
        {
            _text = transform.Find("Text").GetComponent<Text>();
            _icon = transform.Find("Icon").GetComponent<Image>();
            StatId = statId;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetIcon(string iconName)
        {
            var sprite = Resources.Load<Sprite>($"{iconResourcesPath}/{iconName}");
            _icon.sprite = sprite;
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<int, StatBar>
        {
        }
    }
}
