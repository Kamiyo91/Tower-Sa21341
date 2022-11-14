using VortexTower.Miyu.Dice;

namespace VortexTower.Miyu.Cards
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