namespace Battle.Signals
{
    public class BattleRestartedSignal
    {
        public bool WithBuffs { get; }

        public BattleRestartedSignal (bool withBuffs = false)
        {
            WithBuffs = withBuffs;
        }
    }
}
