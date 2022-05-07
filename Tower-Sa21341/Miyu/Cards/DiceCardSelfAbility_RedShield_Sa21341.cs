using VortexLabyrinth_Sa21341.Miyu.Dices;

namespace VortexLabyrinth_Sa21341.Miyu.Cards
{
    public class DiceCardSelfAbility_RedShield_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override string[] Keywords
        {
            get
            {
                return new[]
                {
                    "Healer_Sa21341", "RedShield_Sa21341"
                };
            }
        }

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            owner.allyCardDetail.DrawCards(1);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_DiceRedShield_Sa21341());
        }
    }
}