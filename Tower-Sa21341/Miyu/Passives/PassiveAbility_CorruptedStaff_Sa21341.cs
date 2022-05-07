using LOR_DiceSystem;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_CorruptedStaff_Sa21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash || behavior.Detail == BehaviourDetail.Penetrate ||
                behavior.Detail == BehaviourDetail.Hit)
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = -1
                });
            else
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}