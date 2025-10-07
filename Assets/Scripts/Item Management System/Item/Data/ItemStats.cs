using System;
using System.Reflection;

namespace ItemManagementSystem {
    public enum StatCategory {
        Offense,
        Defense,
        Utility,
        Meta
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StatCategoryAttribute : Attribute {
        public StatCategory Category { get; }
        public StatCategoryAttribute(StatCategory category) => Category = category;
    }
    public static class StatTypeExtensions {
        public static StatCategory GetCategory(this StatType statType) {
            var field = statType.GetType().GetField(statType.ToString());
            var attr = field.GetCustomAttribute<StatCategoryAttribute>();
            return attr != null ? attr.Category : StatCategory.Utility;
        }
    }

    public enum StatType {
        [StatCategory(StatCategory.Meta)] Durability,
        [StatCategory(StatCategory.Meta)] Uses,
        [StatCategory(StatCategory.Meta)] BuffDuration,
        [StatCategory(StatCategory.Meta)] Cooldown,

        [StatCategory(StatCategory.Defense)] Health,
        [StatCategory(StatCategory.Defense)] Defense,
        [StatCategory(StatCategory.Defense)] DamageReduction,

        [StatCategory(StatCategory.Offense)] AttackPower,
        [StatCategory(StatCategory.Offense)] MagicPower,
        [StatCategory(StatCategory.Offense)] CritChance,
        [StatCategory(StatCategory.Offense)] CritDamage,
        [StatCategory(StatCategory.Offense)] LifeSteal,
        [StatCategory(StatCategory.Offense)] SpellVamp,
        [StatCategory(StatCategory.Offense)] AttackSpeed,

        [StatCategory(StatCategory.Utility)] MovementSpeed,
        [StatCategory(StatCategory.Utility)] HealAmount,
        [StatCategory(StatCategory.Utility)] Regeneration,
    }

    [Serializable]
    public struct Stat {
        public StatType type;
        public float value;
    }
}