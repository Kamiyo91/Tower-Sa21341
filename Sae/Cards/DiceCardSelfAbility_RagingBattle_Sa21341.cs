namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_RagingBattle_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (owner.hp > owner.MaxHp * 0.25f) return;
            card.ApplyDiceStatBonus(DiceMatch.AllDefenseDice, new DiceStatBonus
            {
                power = 1
            });
        }
    }
}