using System.Collections.Generic;

namespace VortexLabyrinth_Sa21341.BLL
{
    public static class VortexModParameters
    {
        public const string PackageId = "SaeModSa21341.Mod";
        public static string Path;

        public static List<LorId> HealerCardsId = new List<LorId>
        {
            new LorId(PackageId, 12), new LorId(PackageId, 13), new LorId(PackageId, 22), new LorId(PackageId, 23),
            new LorId(PackageId, 24)
        };
    }
}