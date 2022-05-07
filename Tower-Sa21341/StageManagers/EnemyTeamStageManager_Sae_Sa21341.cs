using CustomMapUtility;
using VortexLabyrinth_Sa21341.Maps;

namespace VortexLabyrinth_Sa21341.StageManagers
{
    public class EnemyTeamStageManager_Sae_Sa21341 : EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap<Sae_Sa21341MapManager>("Sae_Sa21341", false, true,
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