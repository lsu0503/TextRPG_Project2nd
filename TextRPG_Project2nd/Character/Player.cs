using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.Item.Cloth;
using TextRPG_Project2nd.Item.Weapon;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Character
{
    public class ConsumableSlot
    {
        public IStackable consumable;
        public int amount;

        public ConsumableSlot(IStackable _item, int _amount)
        {
            consumable = _item;
            amount = _amount;
        }
    }

    public class Player : ICharacter, IPlayer
    {
        // Level 관련 요소 [가시성을 위해서 따로 모음]
        int level;
        int expMax;
        int expCur;

        public int Level { get { return level; } set { level = value; } }
        public int ExpMax { get { return expMax; } set { expMax = value; } }
        public int ExpCur { get { return expCur; } set { expCur = value; } }

        // ICharacter 구성 요소 [Level 제외]
        string name;
        int hpMax;
        int hpCur;
        bool isDead = false;

        int baseAtk;
        int[] typeAtk;

        int[] defStatus;

        int[] attribute;

        List<StatusEffectSlot> effectList = new List<StatusEffectSlot>();

        public string Name { get { return name; } set { name = value; } }
        public int HpMax { get { return hpMax; } set { hpMax = value; } }
        public int HpCur { get { return hpCur; } set { hpCur = value; } }
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        public int BaseAtk { get { return baseAtk; } set { baseAtk = value; } }
        public int[] TypeAtk { get { return typeAtk; } set { typeAtk = value; } }

        public int[] DefStatus { get { return defStatus; } set { defStatus = value; } }

        public int[] Attribute { get { return attribute; } set { attribute = value; } }

        public List<StatusEffectSlot> EffectList { get { return effectList; } set { effectList = value; } }

        public int TakeDamage(int damage, int type)
        {
            int calcDamage;
            if (type == 3)
                calcDamage = (int)(damage * ((100 - cloth.DefStatus[0]) / (float)(100 + cloth.Attribute[0])));
            else
                calcDamage = (int)(damage * ((100 - cloth.DefStatus[type]) / (float)(100 + cloth.Attribute[0])));

            hpCur -= damage;
            if(hpCur < 0)
            {
                hpCur = 0;
                isDead = true;
            }

            return calcDamage;
        }

        public int TakeHeal(int heal)
        {
            int calcHeal = (int)MathF.Min(heal, hpMax - hpCur);
            hpCur += calcHeal;
            return calcHeal;
        }

        public void TakeEffect(IStatusEffect _effect)
        {
            effectList.Add(new StatusEffectSlot(_effect));
        }

        public void ActivateEffect()
        {
            bool isOver;
            int i = 0;

            while(i < effectList.Count)
            {
                isOver = effectList[i].ActivateEffect(this);

                if (isOver)
                    effectList.RemoveAt(i);

                else
                    i++;
            }
        }

        public void UpdateAttribute()
        {
            if (cloth != null)
            {
                attribute = cloth.Attribute.ToArray();
                defStatus = cloth.DefStatus.ToArray();
            }
            else
            {
                attribute = new int[] { 65, 65, 65 };
                defStatus = new int[] { 0, 0, 0 };
            }

            if (weapon != null)
                typeAtk = weapon.TypeAtk.ToArray();
            else
                typeAtk = new int[] { 65, 65, 65 };

            baseAtk = (int)(100.0f * (MathF.Pow(1.25f, level)));
        }

        public void UpdateHp()
        {
            hpMax = (int)(100.0f * (MathF.Pow(1.25f, level)));
            hpCur = hpMax;
        }

        // IPlayer 구성 요소 [경험치 제외]
        ConsumableSlot[] consumableList = new ConsumableSlot[3];
        ICloth cloth;
        IWeapon weapon;
        IDogma dogma;

        public ConsumableSlot[] ConsumableList { get { return consumableList; } set { consumableList = value; } }
        public ICloth Cloth { get { return cloth; } set { cloth = value; } }
        public IWeapon Weapon { get { return weapon; } set { weapon = value; } }
        public IDogma Dogma { get { return dogma; } set { dogma = value; } }

        public Player(ICloth _cloth, IWeapon _weapon)
        {
            level = 0;
            expCur = 0;
            expMax = 100;

            cloth = _cloth;
            weapon = _weapon;

            UpdateAttribute();
            UpdateHp();
        }

        public void GetExp(int amount)
        {
            if (level < 5)
            {
                expCur += amount;

                if (expCur >= expMax)
                {
                    expCur -= expMax;
                    level++;
                    expMax = (int)((100.0f * MathF.Pow(2.5f, level)) - (50 * MathF.Pow(level, 2)));

                    UpdateHp();
                    UpdateAttribute();

                    if (level >= 5)
                        expCur = -1;
                }
            }
        }

        public bool UseItem(int index)
        {
            consumableList[index].amount--;

            if (consumableList[index].amount <= 0)
            {
                consumableList[index] = null;
                return true;
            }

            else
                return false;
        }

        public void EquipItem(ICloth _cloth)
        {
            cloth = _cloth;

            UpdateAttribute();
        }

        public void EquipItem(IWeapon _weapon)
        {
            weapon = _weapon;

            UpdateAttribute();
        }

        public int EquipConsumable(IStackable _consumable, int index, int _amount)
        {
            int resultAmount;

            if (consumableList[index] != null && ReferenceEquals(_consumable, consumableList[index].consumable))
            {
                resultAmount = (int)MathF.Min(consumableList[index].consumable.MaxAmount - consumableList[index].amount, _amount);

                consumableList[index].amount += resultAmount;
            }

            else
            {
                resultAmount = (int)MathF.Min(_consumable.MaxAmount, _amount);

                if (consumableList[index] != null)
                    GameManager.Instance().storage.GetItemConsumable(consumableList[index].consumable as IItem, consumableList[index].amount);

                consumableList[index] = new ConsumableSlot(_consumable, resultAmount);
            }

            return resultAmount;
        }
    }
}
