using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Sae.Buffs;

namespace VortexLabyrinth_Sa21341.Sae.Passives
{
    public class PassiveAbility_DistortionBlessingPlayer_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.RecoverHP(2);
        }

        public override void OnWaveStart()
        {
            InitDialog();
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DistortionBlessing_Sa21341());
        }

        public override void OnRoundEndTheLast()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DistortionBlessing_Sa21341>())
                owner.bufListDetail.AddBuf(new BattleUnitBuf_DistortionBlessing_Sa21341());
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000010));
                    break;
            }
        }
    }
}