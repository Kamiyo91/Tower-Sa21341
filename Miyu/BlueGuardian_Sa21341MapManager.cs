using CustomMapUtility;
using UnityEngine;

namespace VortexTower.Miyu
{
    public class BlueGuardian_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "BlueGuardianPhase1_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}