namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_RagingEdge_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (owner.hp > owner.MaxHp * 0.25f) return;
            card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus
            {
                power = 1
            });
        }
    }
}