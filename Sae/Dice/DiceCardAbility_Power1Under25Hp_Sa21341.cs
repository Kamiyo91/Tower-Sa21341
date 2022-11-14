namespace VortexTower.Sae.Dice
{
    public class DiceCardAbility_Power1Under25Hp_Sa21341 : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}