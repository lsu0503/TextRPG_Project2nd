using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Action
{
    public class EffectAtk
    {
        public IStatusEffect effect;
        public float accuracy;

        public EffectAtk(IStatusEffect _effect, float _accuracy)
        {
            effect = _effect;
            accuracy = _accuracy;
        }

        public IStatusEffect AttackEffect()
        {
            if (new Random().NextDouble() < (double)accuracy)
                return effect;
            else
                return null;
        }
    }

    public class ResultBlock
    {
        public int damage;
        public int heal;
        public List<IStatusEffect> buffList;
        public List<IStatusEffect> badStatusList;
        public int ember;

        public ResultBlock(int _damage, int _heal, List<IStatusEffect> _buffList, List<IStatusEffect> _badStatusList, int _ember)
        {
            damage = _damage;
            heal = _heal;
            buffList = _buffList;
            badStatusList = _badStatusList;
            ember = _ember;
        }
    }

    public interface IAction
    {
        int ActionType { get; } // 0: 기본 공격 or 아이템 | 1: 스킬 | 2: 마법
        string Name { get; }
        string[] DetailAction { get; }
        int Power { get; }

        List<EffectAtk> EffectAtkList { get; }
        List<IStatusEffect> EffectList { get; }

        public bool AttemptAction(ICharacter user);

        public ResultBlock UseAction(ICharacter user, ICharacter target);
    }

    public interface IAttack
    {
        int TurnCounterCurrent { get; set; }
        int TurnCounterMax { get; }
    }

    public interface ISkill
    {
        int SkillCounterCurrent { get; set; }
        int SkillCounterMax { get; }
    }

    public interface IMagic
    {
        int EmberCost { get; }
    }
}
