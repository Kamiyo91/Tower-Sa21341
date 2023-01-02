using CustomMapUtility;
using UnityEngine;

namespace VortexTower.Sae
{
    public class SaePhase1_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "SaePhase1_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}