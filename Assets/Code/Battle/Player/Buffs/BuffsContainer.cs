using UnityEngine;

namespace Battle.Player.Buffs
{
    class BuffsContainer
    {
        public Buff[] Buffs { get; private set; }
        public BuffsContainer(Buff[] buffs)
        {
            Buffs = buffs;
        }
    }
}
