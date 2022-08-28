using System.Linq;
using CustomMapUtility;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.GreenHunter.Passives;
using VortexLabyrinth_Sa21341.Maps;

namespace VortexLabyrinth_Sa21341.StageManagers
{
    public class EnemyTeamStageManager_GreenGuardian_Sa21341 : EnemyTeamStageManager
    {
        private PassiveAbility_GreenGuardian_Sa21341 _passive;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap<GreenGuardian_Sa21341MapManager>("GreenHunter_Sa21341", false, true,
                0.5f, 0.25f, 0.5f, 0.8f);
            CustomMapHandler.InitCustomMap<GreenGuardian2_Sa21341MapManager>("GreenHunterPhase2_Sa21341", false, true,
                0.5f, 0.2f, 0.5f, 0.25f);
            CustomMapHandler.EnforceMap();
            _passive = BattleObjectManager.instance.GetAliveList(Faction.Enemy).FirstOrDefault()?.passiveDetail
                .PassiveList
                .FirstOrDefault(x => x is PassiveAbility_GreenGuardian_Sa21341) as PassiveAbility_GreenGuardian_Sa21341;
            Singleton<StageController>.Instance.CheckMapChange();
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id !=
                new LorId(VortexModParameters.PackageId, 5)) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                unit.bufListDetail.AddBuf(new BattleUnitBuf_Vip_Sa21341());
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap(_passive?.GetPhase() ?? 0);
        }
    }
}