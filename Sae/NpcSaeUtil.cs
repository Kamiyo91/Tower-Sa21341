using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using UI;

namespace VortexTower.Sae
{
    public class NpcSaeUtil : NpcMechUtilBase
    {
        public NpcSaeUtil(NpcMechUtilBaseModel model) : base(model, VortexModParameters.PackageId)
        {
        }

        public override void ExtraMechRoundPreEnd(MechPhaseOptions mechOptions)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Model.Owner.faction)
                         .Where(x => x != Model.Owner))
                unit.Die();
            if (StageController.Instance.GetStageModel().ClassInfo.id !=
                new LorId(VortexModParameters.PackageId, 1)) return;
            var playerUnit = BattleObjectManager.instance
                .GetAliveList(UnitUtil.ReturnOtherSideFaction(Model.Owner.faction))
                .FirstOrDefault();
            playerUnit?.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 14));
            UIAlarmPopup.instance.SetAlarmText(ModParameters.LocalizedItems[VortexModParameters.PackageId]
                .EffectTexts["SaeFightWarning_Sa21341"].Desc);
        }
    }
}