using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;

namespace VortexTower.Miyu
{
    public class EnemyTeamStageManager_BlueGuardian_Sa21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(new MiyuUtil().MiyuNpcUtil, new List<MapModel> { VortexModParameters.MiyuMap });
            base.OnWaveStart();
        }
    }
}