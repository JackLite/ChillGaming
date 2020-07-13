using UnityEngine;

namespace Battle.UI
{
    class Health
    {
        private float _amount;
        private HealthBar _healthBar;

        public Health(float amount, HealthBar healthBar)
        {
            _amount = amount;
            _healthBar = healthBar;
        }

        public void Change(float changeValue)
        {
            _amount += changeValue;
        }
    }
}
