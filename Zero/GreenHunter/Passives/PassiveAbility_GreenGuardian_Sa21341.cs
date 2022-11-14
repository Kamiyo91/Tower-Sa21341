using System.Linq;
using BigDLL4221.Passives;

namespace VortexTower.Zero.GreenHunter.Passives
{
    public class PassiveAbility_GreenGuardian_Sa21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            Util.Model.PermanentBuffList.FirstOrDefault()?.Buff?.OnAddBuf(1);
        }

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new GreenGuardianUtil().GreenGuardianNpcUtil);
        }

        public override void OnWaveStart()
        {
            var passive = owner.passiveDetail.AddPassive(new PassiveAbility_251201());
            passive.Hide();
            base.OnWaveStart();
        }
    }
}