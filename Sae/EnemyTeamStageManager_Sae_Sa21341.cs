using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;

namespace VortexTower.Sae
{
    public class EnemyTeamStageManager_Sae_Sa21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(new SaeUtil().SaeNpcUtil,
                new List<MapModel> { VortexModParameters.SaePhase1Map, VortexModParameters.SaePhase2Map });
            base.OnWaveStart();
        }
    }
}