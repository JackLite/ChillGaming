namespace Battle.Player.Buffs
{
    class BuffsContainer
    {
        public Buff[] Buffs { get; }
        public BuffsContainer(Buff[] buffs) => Buffs = buffs;
    }
}
