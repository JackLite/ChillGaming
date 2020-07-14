using System;

[Serializable]
public class Data
{
    public GameModel settings;
    public Stat[] stats;
    public Buff[] buffs;
}

[Serializable]
public class GameModel
{
    public int buffCountMin;
    public int buffCountMax;
    public bool allowDuplicateBuffs;

}

[Serializable]
public class Stat : ICloneable
{
    public int id;
    public string title;
    public string icon;
    public float value;

    public Stat()
    {
    }

    public Stat (int id, string title, string icon, float value)
    {
        this.id = id;
        this.title = title;
        this.icon = icon;
        this.value = value;
    }

    public object Clone()
    {
        return new Stat(id, title, icon, value);
    }
}

[Serializable]
public class BuffStat
{
    public float value;
    public int statId;
}

[Serializable]
public class Buff
{
    public string icon;
    public int id;
    public string title;
    public BuffStat[] stats;
}
