using BigDLL4221.Utils;
using UnityEngine;

namespace VortexTower.Sae
{
    public class Sae_Sa21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "Sae_Sa21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}