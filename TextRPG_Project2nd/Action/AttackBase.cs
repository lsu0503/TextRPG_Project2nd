using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Action
{
    internal class AttackBase: IAction, IAttack
    {
        // IAction 구성요소
        int actionType = 0;
        string name;
        string[] detailAction;
        int power;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>();

        public int ActionType { get { return actionType; } }
        public string Name { get { return name; } }
        public string[] DetailAction { get { return detailAction; } }
        public int Power { get { return power; } }

        public List<EffectAtk> EffectAtkList { get { return effectAtkList; } }
        public List<IStatusEffect> EffectList { get { return effectList; } }

        public bool AttemptAction(ICharacter user)
        {
            turnCountCurrent++;

            if (turnCountCurrent >= turnCountMax)
            {
                turnCountCurrent = 0;
                return true;
            }

            else
                return false;

        }

        public ResultBlock UseAction(ICharacter user, ICharacter target)
        {
            int damage = (int)(power * user.BaseAtk / 100.0f * user.TypeAtk[0] / 100.0f);

            return new ResultBlock(damage, 0, null, null, 0);
        }

        // IAttack 구성요소
        int turnCountCurrent;
        int turnCountMax;

        public int TurnCounterCurrent { get; set; }
        public int TurnCounterMax { get; }

        public AttackBase(string _name, int _power, List<EffectAtk> _effectAtkList, int _turnCountMax)
        {
            name = _name; 
            power = _power;
            if(_effectAtkList != null)
                effectAtkList = _effectAtkList.ToList();
            turnCountMax = 0;
            turnCountCurrent = turnCountMax;
        }
    }
}
