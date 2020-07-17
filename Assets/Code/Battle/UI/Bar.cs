using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    class Bar : MonoBehaviour
    {
        protected const string iconResourcesPath = "Icons";
        protected Text _text;
        protected Image _icon;

        protected void InitUIComponents()
        {
            _text = transform.Find("Text").GetComponent<Text>();
            _icon = transform.Find("Icon").GetComponent<Image>();
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
    }
}
