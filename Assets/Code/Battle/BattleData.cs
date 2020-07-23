using Leguar.TotalJSON;
using System;
using Zenject;

namespace Battle
{
    public class BattleData
    {
        private Data _data;

        public Data Data
        {
            get
            {
                if (_data == null) throw new Exception("Data is not parsing yet");
                return _data;
            }
            private set => _data = value;
        }

        [Inject(Id = "BattleSettings")]
        public BattleData(string settingsJson)
        {
            Data = new Data();
            var json = JSON.ParseString(settingsJson);
            Data.settings = ReadGameModel(json);
            Data.stats = ReadStats(json);
            Data.buffs = ReadBuffs(json);
        }

        private GameModel ReadGameModel(JSON json)
        {
            return json.GetJSON("settings").Deserialize<GameModel>();
        }

        private Stat[] ReadStats(JSON json)
        {
            var jsonStats = json.GetJArray("stats").AsJSONArray();
            var stats = new Stat[jsonStats.Length];
            for (var i = 0; i < stats.Length; i++)
            {
                stats[i] = jsonStats[i].Deserialize<Stat>();
            }
            return stats;
        }

        private Buff[] ReadBuffs(JSON json)
        {
            var jsonBuffs = json.GetJArray("buffs").AsJSONArray();
            var buffs = new Buff[jsonBuffs.Length];
            for (var i = 0; i < buffs.Length; i++)
            {
                buffs[i] = jsonBuffs[i].Deserialize<Buff>();
            }
            return buffs;
        }
    }
}