using UnityEngine;

namespace Battle.Signals
{
    class HealthChangeSignal
    {
        public float Amount { get; private set; }

        public HealthChangeSignal(float amount)
        {
            Amount = amount;
        }
    }
}
