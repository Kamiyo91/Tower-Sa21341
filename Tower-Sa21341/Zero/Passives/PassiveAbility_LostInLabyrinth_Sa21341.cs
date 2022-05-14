namespace VortexLabyrinth_Sa21341.Zero.Passives
{
    public class PassiveAbility_LostInLabyrinth_Sa21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.RecoverHP(2);
        }
    }
}