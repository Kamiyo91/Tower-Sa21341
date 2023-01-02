using CustomMapUtility;
using UnityEngine;

namespace VortexTower.Zero
{
    public class GreenGuardian_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "GreenGuardian_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}