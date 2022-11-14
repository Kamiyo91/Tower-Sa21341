using BigDLL4221.Passives;

namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_MiyuNpc_Sa21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MiyuUtil().MiyuNpcUtil);
        }
    }
}