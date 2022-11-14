using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;

namespace VortexTower.Zero.GreenHunter
{
    public class NpcMechUtil_GreenGuardian : NpcMechUtilBase
    {
        public NpcMechUtil_GreenGuardian(NpcMechUtilBaseModel model) : base(model)
        {
        }

        public override void ExtraMethodOnPhaseChangeRoundStart(MechPhaseOptions mechOptions)
        {
            Model.PermanentBuffList.FirstOrDefault()?.Buff?.OnAddBuf(10);
        }
    }
}