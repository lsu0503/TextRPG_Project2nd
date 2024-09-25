using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG_Project2nd.Action.Skill
{
    internal class SkillBash : IAction, ISkill
    {
        // IAction 구성요소
        int actionType= 1;
        string name = "강베기";
        string[] detailAction = new string[] { "온 몸의 힘을 실어 베어내는 공격.",
                                               "강력한 피해를 입히지만 자주 쓰지는 못한다." };
        int power = 35;

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
            skillCounterCurrent += user.Attribute[1];

            if (skillCounterCurrent > skillCounterMax)
            {
                skillCounterCurrent -= skillCounterMax;
                return true;
            }

            else
                return false;
        }

        public ResultBlock UseAction(ICharacter user, ICharacter target)
        {
            int damage = (int)(power * user.BaseAtk / 100.0f * user.TypeAtk[1] / 100.0f);

            return new ResultBlock(damage, 0, null, null, 0);
        }

        // ISkill 구성 요소
        int skillCounterCurrent;
        int skillCounterMax = 650;

        public int SkillCounterCurrent { get { return skillCounterCurrent; } set { skillCounterCurrent = value; } }
        public int SkillCounterMax { get { return skillCounterMax; } }

        public SkillBash()
        {
            skillCounterCurrent = 0;
        }
    }
}
