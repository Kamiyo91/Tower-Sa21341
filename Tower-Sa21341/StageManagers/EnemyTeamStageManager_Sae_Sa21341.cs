using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.CustomMapUtility.Assemblies;

namespace VortexLabyrinth_Sa21341.StageManagers
{
    public class EnemyTeamStageManager_Sae_Sa21341 : EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Sae_Sa21341", typeof(Sae_Sa21341MapManager), false, true,
                0.5f, 0.25f, 0.5f, 0.8f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }
    }
}