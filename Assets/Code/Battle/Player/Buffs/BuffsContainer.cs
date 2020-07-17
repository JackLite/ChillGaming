namespace Battle.Player.Buffs
{
    public class BuffsContainer
    {
        public Buff[] Buffs { get; }
        public BuffsContainer(Buff[] buffs) => Buffs = buffs;
    }
}
