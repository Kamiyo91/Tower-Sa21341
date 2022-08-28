using CustomMapUtility;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.Maps.ForgottenMaps
{
    public class Forgotten5_Sa21341MapManager : CustomMapManager
    {
        protected internal override string[] CustomBGMs => new[] { "KamiyoPhase_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}