namespace VortexTower.Forgotten.MioShadow.Passives
{
    public class PassiveAbility_ForgottenLink_Sa21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 1);
        }
    }
}