using VortexLabyrinth_Sa21341.Miyu;

namespace VortexLabyrinth_Sa21341.Aztec.Cards
{
    public class DiceCardSelfAbility_AztecShield_Sa21341 : DiceCardSelfAbility_MiyuCommonCard_Sa21341
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            var target = card.target;
            target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1);
            target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1);
        }
    }
}