using CustomMapUtility;
using UnityEngine;

namespace VortexTower.Forgotten.ForgottenMaps
{
    public class Forgotten2_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "WiltonPhase_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}