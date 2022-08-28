using CustomMapUtility;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.Maps.ForgottenMaps
{
    public class Forgotten1_Sa21341MapManager : CustomMapManager
    {
        protected internal override string[] CustomBGMs => new[] { "SectionPhaseStart_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}