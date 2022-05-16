using System.Collections.Generic;

namespace VortexLabyrinth_Sa21341.BLL
{
    public static class VortexModParameters
    {
        public const string PackageId = "SaeModSa21341.Mod";
        public static string Path;

        public static List<LorId> SaeKeypageIds = new List<LorId>
            { new LorId(PackageId, 10000901), new LorId(PackageId, 10000010), new LorId(PackageId, 10000003) };

        public static List<LorId> IgnoredCombatCards = new List<LorId>
            { new LorId(PackageId, 67), new LorId(PackageId, 68) };
    }
}