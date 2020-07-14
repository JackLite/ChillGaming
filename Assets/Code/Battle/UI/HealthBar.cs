using Battle.Player;
using Battle.Signals;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class HealthBar : MonoBehaviour
    {
        private PlayerController _player;

        private Text _text;
        private Image _border;
        private float _startHealth;

        [Inject]
        public void Init(PlayerController player)
        {
            _player = player;
            _startHealth = _player.GetHealth(); 
            _text = GetComponentInChildren<Text>();
            _border = GetComponent<Image>();
            UpdateUI();
        }

        public void OnHealthChange(HealthChangeSignal signal)
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            _text.text = _player.GetHealth().ToString();
            _border.fillAmount = _player.GetHealth() / _startHealth;
        }
    }
}
