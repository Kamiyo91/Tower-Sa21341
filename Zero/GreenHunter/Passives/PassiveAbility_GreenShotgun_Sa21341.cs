using LOR_DiceSystem;

namespace VortexTower.Zero.GreenHunter.Passives
{
    public class PassiveAbility_GreenShotgun_Sa21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.card?.card?.XmlData?.Spec?.Ranged == CardRange.Far)
                behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }
    }
}