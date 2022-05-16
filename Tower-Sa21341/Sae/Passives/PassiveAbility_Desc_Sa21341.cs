using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Sae.Passives
{
    public class PassiveAbility_Desc_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            InitDialog();
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 1:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000901));
                    break;
                case 3:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000003));
                    break;
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000010));
                    break;
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000012));
                    break;
            }
        }
    }
}