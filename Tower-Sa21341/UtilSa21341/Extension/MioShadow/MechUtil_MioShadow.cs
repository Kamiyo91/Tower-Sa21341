using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension.MioShadow
{
    public class MechUtil_MioShadow : MechUtilEx
    {
        private readonly MechUtilBaseModel _model;

        public MechUtil_MioShadow(MechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public static int AlwaysAimToTheSlowestDice(BattleUnitModel target)
        {
            var speedValue = 999;
            var finalTarget = 0;
            foreach (var dice in target.speedDiceResult.Select((x, i) => new { i, x }))
            {
                if (speedValue <= dice.x.value) continue;
                speedValue = dice.x.value;
                finalTarget = dice.i;
            }

            return finalTarget;
        }
    }
}