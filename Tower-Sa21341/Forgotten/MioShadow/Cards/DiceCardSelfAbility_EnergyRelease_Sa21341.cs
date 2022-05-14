namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Cards
{
    public class DiceCardSelfAbility_EnergyRelease_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, owner);
        }

        public override void OnStartBattle()
        {
            owner.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
        }
    }
}