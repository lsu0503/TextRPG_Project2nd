using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Action.Skill
{
    internal class SkillBite : IAction, ISkill
    {
        // IAction 구성요소
        int actionType= 1;
        string name = "물어뜯기";
        string[] detailAction = new string[] { "날렵하게 달려들어 목덜미를 물어뜯는다.",
                                               "... 스러져 가는 몸에는 약점이 없다.",
                                               "그것은 저들과 우리 모두에게 재앙이었다."};
        int power = 20;

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
            int damage = (int)(power * user.BaseAtk / 100.0f * user.TypeAtk[0] / 100.0f);

            return new ResultBlock(damage, 0, null, null, 0);
        }

        // ISkill 구성 요소
        int skillCounterCurrent;
        int skillCounterMax = 350;

        public int SkillCounterCurrent { get { return skillCounterCurrent; } set { skillCounterCurrent = value; } }
        public int SkillCounterMax { get { return skillCounterMax; } }

        public SkillBite()
        {
            skillCounterCurrent = 0;
        }
    }
}
