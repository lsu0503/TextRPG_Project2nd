using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Character
{
    public class ItemDrop
    {
        public IItem item;
        public float prob;

        public ItemDrop(IItem _item, float _prob)
        {
            item = _item;
            prob = _prob;
        }

        public bool CheckDrop()
        {
            if (new Random().NextDouble() < prob)
                return true;
            else
                return false;
        }
    }

    public interface ICharacter
    {
        string Name { get; set; }
        int HpMax { get; set; }
        int HpCur { get; set; }
        bool IsDead { get; set; }

        int Level { get; set; }

        int BaseAtk { get; set; }
        int[] TypeAtk { get; set; }
        int[] DefStatus { get; set; }
        int[] Attribute { get; set; }

        List<StatusEffectSlot> EffectList { get; set; }

        public int TakeDamage(int damage, int actionType);

        public int TakeHeal(int cure);

        public void TakeEffect(IStatusEffect _effect);

        public void ActivateEffect();

        public void UpdateAttribute();

        public void UpdateHp();
    }

    public interface IMonster
    {
        int OriginHp { get; }
        int OriginAtk { get; }

        int[] OriginTypeAtk { get; }
        int[] OriginDefStatus { get; }
        int[] OriginAttribute { get; }

        int EmberDrop { get; }
        int AmberDrop { get; }
        List<ItemDrop> ItemDropList { get; }
        int ExpDrop {  get; }

        IAttack Attack { get; }
        List<ISkill> SkillList { get; }
    }

    public interface IPlayer
    {
        int ExpCur { get; set; }
        int ExpMax { get; set; }

        ConsumableSlot[] ConsumableList { get; set; }
        ICloth Cloth { get; set; }
        IWeapon Weapon { get; set; }
        IDogma Dogma { get; set; }

        public void GetExp(int amount);

        public bool UseItem(int index);

        public void EquipItem(ICloth _cloth);

        public void EquipItem(IWeapon _weapon);

        public int EquipConsumable(IStackable _consumable, int index, int _amount);
    }
}
