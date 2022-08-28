namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Passives
{
    public class PassiveAbility_ForgottenLink_Sa21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength,3);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 3);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 3);
        }
    }
}
