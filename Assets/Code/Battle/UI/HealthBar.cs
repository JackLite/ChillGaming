using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    [RequireComponent(typeof(Image))]
    class HealthBar : MonoBehaviour
    { 
        private Image _image;

        public void Awake()
        {
            _image = GetComponent<Image>();
        }

    }
}
