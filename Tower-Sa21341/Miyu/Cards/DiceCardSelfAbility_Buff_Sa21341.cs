using VortexLabyrinth_Sa21341.Miyu.Dices;

namespace VortexLabyrinth_Sa21341.Miyu.Cards
{
    public class DiceCardSelfAbility_Buff_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            var target = card.target;
            target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1);
            target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_HealDice_Sa21341());
        }
    }
}