namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_Struggle_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            if (owner.hp > owner.MaxHp * 0.25f) return;
            owner.allyCardDetail.DrawCards(1);
        }
    }
}