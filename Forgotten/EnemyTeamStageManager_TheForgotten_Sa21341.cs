using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using CustomMapUtility;
using VortexTower.Forgotten.KamiyoShadow.Passives;

namespace VortexTower.Forgotten
{
    public class EnemyTeamStageManager_TheForgotten_Sa21341 : EnemyTeamStageManager_BaseWithCMUOnly_DLL4221
    {
        private PassiveAbility_ForgottenEgo_Sa_21341 _passive;

        public override void OnWaveStart()
        {
            SetParameters(CustomMapHandler.GetCMU(VortexModParameters.PackageId),
                new List<MapModel>
                {
                    VortexModParameters.ForgottenMap1, VortexModParameters.ForgottenMap2,
                    VortexModParameters.ForgottenMap3, VortexModParameters.ForgottenMap4,
                    VortexModParameters.ForgottenMap5
                });
            base.OnWaveStart();
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id !=
                new LorId(VortexModParameters.PackageId, 7)) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                unit.bufListDetail.AddBuf(new BattleUnitBuf_Vip_Sa21341());
        }

        public override void OnRoundStart()
        {
            if (_passive == null)
                _passive = BattleObjectManager.instance.GetAliveList(Faction.Enemy)
                        .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgo_Sa_21341>())?
                        .passiveDetail.PassiveList
                        .FirstOrDefault(x => x is PassiveAbility_ForgottenEgo_Sa_21341) as
                    PassiveAbility_ForgottenEgo_Sa_21341;
            ChangePhase(_passive?.GetPhase() ?? 0);
            base.OnRoundStart();
        }
    }
}