namespace VortexLabyrinth_Sa21341.Zero.Dices
{
    public class DiceCardAbility_BlueHorizonDice_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            owner.RecoverHP(4);
        }
    }
}