using System.Collections.Generic;
using System.Linq;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

internal class StackableBuffTracker : ModPlayer
{
    internal class BuffData
    {
        public float Strength => time <= 1 ? 0 : strength;

        public int time;
        public float strength;

        public BuffData(int time, float strength)
        {
            this.time = time;
            this.strength = strength;
        }
    }

    private readonly Dictionary<string, List<BuffData>> _stackedBuffs = new();
    private readonly Dictionary<string, int> _stackedBuffTypes = new();

    public void StackableBuff<T>(string name, BuffData data) where T : ModBuff
    {
        if (_stackedBuffs.ContainsKey(name))
            _stackedBuffs[name].Add(data);
        else
        {
            _stackedBuffs.Add(name, new List<BuffData>() { data });
            _stackedBuffTypes.Add(name, ModContent.BuffType<T>());
        }
    }

    public float StackableStrength(string name)
    {
        if (!_stackedBuffs.ContainsKey(name))
            return 0;

        float strength = 0f;

        foreach (var item in _stackedBuffs[name])
            strength += item.Strength;

        return strength;
    }

    public override void PostUpdateBuffs()
    {
        List<string> removals = new();

        foreach (var key in _stackedBuffs.Keys)
        {
            var buffStack = _stackedBuffs[key];

            for (int i = 0; i < buffStack.Count; i++)
            {
                BuffData buff = buffStack[i];
                buff.time--;
            }

            buffStack.RemoveAll(x => x.time <= 0);

            if (buffStack.Count <= 0)
                removals.Add(key);
            else
                Player.AddBuff(_stackedBuffTypes[key], buffStack.Max(x => x.time));
        }

        foreach (var item in removals)
        {
            _stackedBuffs.Remove(item);
            _stackedBuffTypes.Remove(item);
        }
    }

    public override void OnEnterWorld() => _stackedBuffs.Clear();
}
