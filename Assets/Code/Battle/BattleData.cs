using Leguar.TotalJSON;
using System;
using UnityEngine;
using Zenject;

namespace Battle
{
    class BattleData : IInitializable
    {
        private TextAsset _settings;

        private Data _data;

        public Data data
        {
            get
            {
                if (_data == null) throw new Exception("Data is not parsing yet");
                return _data;
            }
            private set => _data = value;
        }

        [Inject(Id = "BattleSettings")]
        public BattleData(TextAsset settings)
        {
            _settings = settings;
        }

        //TODO Add test with different settings
        public void Initialize()
        {
            data = new Data();
            var json = JSON.ParseString(_settings.text);
            data.settings = ReadGameModel(json);
            data.stats = ReadStats(json);
            data.buffs = ReadBuffs(json);
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