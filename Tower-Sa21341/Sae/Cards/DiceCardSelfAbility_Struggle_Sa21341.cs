namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_Struggle_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}