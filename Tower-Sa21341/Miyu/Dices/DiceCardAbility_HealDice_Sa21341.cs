namespace VortexLabyrinth_Sa21341.Miyu.Dices
{
    public class DiceCardAbility_HealDice_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            target.breakDetail.RecoverBreak(behavior.DiceResultValue);
            target.RecoverHP(behavior.DiceResultValue);
        }
    }
}