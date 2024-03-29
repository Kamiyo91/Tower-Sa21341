﻿using VortexTower.Miyu.Dice;

namespace VortexTower.Miyu.Cards
{
    public class DiceCardSelfAbility_YellowShield_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override string[] Keywords
        {
            get
            {
                return new[]
                {
                    "Healer_Sa21341", "YellowShield_Sa21341"
                };
            }
        }

        public override void OnUseCard()
        {
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_DiceYellowShield_Sa21341());
        }
    }
}