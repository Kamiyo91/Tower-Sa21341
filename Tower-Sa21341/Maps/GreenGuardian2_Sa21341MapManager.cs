using UnityEngine;
using VortexLabyrinth_Sa21341.UtilSa21341.CustomMapUtility.Assemblies;

namespace VortexLabyrinth_Sa21341.Maps
{
    public class GreenGuardian2_Sa21341MapManager : CustomMapManager
    {
        protected internal override string[] CustomBGMs => new[] { "GreenGuardian2_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}