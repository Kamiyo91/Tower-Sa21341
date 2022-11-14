using BigDLL4221.Utils;
using UnityEngine;

namespace VortexTower.Forgotten.ForgottenMaps
{
    public class Forgotten1_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "SectionPhaseStart_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}