using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.CustomMapUtility.Assemblies;

namespace VortexLabyrinth_Sa21341.StageManagers
{
    public class EnemyTeamStageManager_BlueGuardian_Sa21341 : EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("BlueGuardian_Sa21341", typeof(BlueGuardian_Sa21341MapManager), false, true,
                0.5f, 0.25f, 0.5f, 0.8f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id !=
                new LorId(VortexModParameters.PackageId, 3)) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                unit.bufListDetail.AddBuf(new BattleUnitBuf_Vip_Sa21341());
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }
    }
}