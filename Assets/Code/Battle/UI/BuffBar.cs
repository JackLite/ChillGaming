using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class BuffBar : MonoBehaviour
    {
        private const string iconResourcesPath = "Icons";
        private Text _text;
        private Image _icon;

        [Inject]
        public void Init()
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

        public class Factory : PlaceholderFactory<BuffBar>
        {
        }
    }
}
