using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Passives
{
    public class PassiveAbility_BlueBurn_Sa21341 : PassiveAbilityBase
    {
        private bool _check;

        public override void OnRoundStartAfter()
        {
            var unit = BattleObjectManager.instance.GetAliveList()
                .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_Zero_Sa21341>());
            if (unit == null || unit.IsDead()) _check = true;
            else _check = false;
        }

        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            if (_check || buf.bufType != KeywordBuf.Burn || buf is BattleUnitBuf_BlueBurn_Sa21341) return true;
            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_BlueBurn_Sa21341) is
                BattleUnitBuf_BlueBurn_Sa21341 buff)
                buff.OnAddBuf(buf.stack);
            else
                owner.bufListDetail.AddBuf(new BattleUnitBuf_BlueBurn_Sa21341());
            return false;
        }
    }
}