using BigDLL4221.Utils;
using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_LoneWarrior_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_230008) is PassiveAbility_230008
                passiveLone)
                owner.passiveDetail.DestroyPassive(passiveLone);
        }

        public override void OnRoundEnd()
        {
            if (UnitUtil.SupportCharCheck(owner) > 1) return;
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>() ||
                owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralAtkStance_Sa21341>())
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 3);
            else if (owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>() ||
                     owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralDefStance_Sa21341>())
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 3);
        }
    }
}