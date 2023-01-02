﻿using System.Linq;
using CustomMapUtility;
using VortexTower.Forgotten.ForgottenMaps;
using VortexTower.Forgotten.KamiyoShadow.Passives;

namespace VortexTower.Forgotten
{
    public class EnemyTeamStageManager_TheForgotten_Sa21341 : EnemyTeamStageManager
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(VortexModParameters.PackageId);
        private PassiveAbility_ForgottenEgo_Sa_21341 _passive;

        public override void OnWaveStart()
        {
            _cmh.InitCustomMap<Forgotten1_Sa21341MapManager>("Forgotten1_Sa21341", false, true,
                0.5f,
                0.55f);
            _cmh.InitCustomMap<Forgotten2_Sa21341MapManager>("Forgotten2_Sa21341", false, true,
                0.5f, 0.2f);
            _cmh.InitCustomMap<Forgotten3_Sa21341MapManager>("Forgotten3_Sa21341", false, true,
                0.5f, 0.2f);
            _cmh.InitCustomMap<Forgotten4_Sa21341MapManager>("Forgotten4_Sa21341", false, true,
                0.5f, 0.3f,
                0.5f, 0.475f);
            _cmh.InitCustomMap<Forgotten5_Sa21341MapManager>("Forgotten5_Sa21341", false, true,
                0.5f,
                0.475f, 0.5f, 0.225f);
            _cmh.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
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
            _cmh.EnforceMap(_passive?.GetPhase() ?? 0);
        }
    }
}