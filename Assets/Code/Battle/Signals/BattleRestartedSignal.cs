namespace Battle.Signals
{
    class BattleRestartedSignal
    {
        public bool WithBuffs { get; }

        public BattleRestartedSignal (bool withBuffs = false)
        {
            WithBuffs = withBuffs;
        }
    }
}
