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
    internal class SkillRelaxTension : IAction, ISkill
    {
        // IAction 구성요소
        int actionType= 1;
        string name = "긴장 풀기";
        string[] detailAction = new string[] { "익숙한 몸동작을 통해서 온 몸의 긴장을 푼다.",
                                               "짧은 시간, 공격력이 상승한다." };
        int power = -1;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>() { new BuffAttack(3, 20) };

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
            return new ResultBlock(0, 0, effectList, null, 0);
        }

        // ISkill 구성 요소
        int skillCounterCurrent;
        int skillCounterMax = 350;

        public int SkillCounterCurrent { get { return skillCounterCurrent; } set { skillCounterCurrent = value; } }
        public int SkillCounterMax { get { return skillCounterMax; } }

        public SkillRelaxTension()
        {
            skillCounterCurrent = 0;
        }
    }
}
