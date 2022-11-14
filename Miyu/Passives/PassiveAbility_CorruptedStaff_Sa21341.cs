using LOR_DiceSystem;

namespace VortexTower.Miyu.Passives
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

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            if (target == null || target.faction != owner.faction) return;
            target.RecoverHP(2);
            target.breakDetail.RecoverBreak(2);
        }
    }
}