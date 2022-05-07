using VortexLabyrinth_Sa21341.BluePetal.Dices;

namespace VortexLabyrinth_Sa21341.BluePetal.Cards
{
    public class DiceCardSelfAbility_PetalDrawAndLight_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_BluePetal_Sa21341());
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}