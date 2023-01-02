using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using CustomMapUtility;

namespace VortexTower.Zero
{
    public class EnemyTeamStageManager_GreenGuardian_Sa21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(CustomMapHandler.GetCMU(VortexModParameters.PackageId),
                new GreenGuardianUtil().GreenGuardianNpcUtil,
                new List<MapModel> { VortexModParameters.ZeroMap });
            base.OnWaveStart();
        }
    }
}