using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Buffs;
using BigDLL4221.Models;
using BigDLL4221.Utils;

namespace VortexTower.Zero
{
    public class MechUtil_Zero : MechUtilBase
    {
        public MechUtil_Zero(MechUtilBaseModel model) : base(model)
        {
        }

        public override void SurviveCheck(int dmg)
        {
            if (Model.Owner.hp - dmg > Model.SurviveHp || !Model.Survive) return;
            var buff = Model.PermanentBuffList.FirstOrDefault();
            Model.RecoverToHp = buff != null ? 10 + buff.Buff.stack * 3 : 10;
            Model.Survive = false;
            UnitUtil.UnitReviveAndRecovery(Model.Owner, 0, Model.RecoverLightOnSurvive);
            if (Model.SurviveAbDialogList.Any())
                UnitUtil.BattleAbDialog(Model.Owner.view.dialogUI, Model.SurviveAbDialogList,
                    Model.SurviveAbDialogColor);
            Model.Owner.SetHp(Model.RecoverToHp);
            Model.Owner.bufListDetail.AddBufWithoutDuplication(
                new BattleUnitBuf_ImmunityToStatusAlimentType_DLL4221());
            Model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL4221());
            buff?.Buff?.OnAddBuf(-99);
        }
    }
}