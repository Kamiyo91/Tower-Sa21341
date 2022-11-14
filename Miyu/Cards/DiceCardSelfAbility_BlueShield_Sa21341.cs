using VortexTower.Miyu.Dice;

namespace VortexTower.Miyu.Cards
{
    public class DiceCardSelfAbility_BlueShield_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override string[] Keywords
        {
            get
            {
                return new[]
                {
                    "Healer_Sa21341", "BlueShield_Sa21341"
                };
            }
        }

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_DiceBlueShield_Sa21341());
        }
    }
}