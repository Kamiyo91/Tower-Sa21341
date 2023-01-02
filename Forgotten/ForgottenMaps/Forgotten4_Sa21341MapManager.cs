using CustomMapUtility;
using UnityEngine;

namespace VortexTower.Forgotten.ForgottenMaps
{
    public class Forgotten4_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "HayatePhase_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}