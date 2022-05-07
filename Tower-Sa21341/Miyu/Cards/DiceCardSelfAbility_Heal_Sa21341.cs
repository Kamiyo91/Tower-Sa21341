using VortexLabyrinth_Sa21341.Miyu.Dices;

namespace VortexLabyrinth_Sa21341.Miyu.Cards
{
    public class DiceCardSelfAbility_Heal_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_HealDice_Sa21341());
        }
    }
}