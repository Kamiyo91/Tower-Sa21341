using System.Linq;
using BigDLL4221.Utils;
using VortexTower.Miyu.Passives;

namespace VortexTower.Miyu.Aztec.Passives
{
    public class PassiveAbility_AztecShield_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 45, VortexModParameters.PackageId);
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            if (card.GetID() != new LorId(VortexModParameters.PackageId, 27) &&
                card.GetID() != new LorId(VortexModParameters.PackageId, 28)) return base.ChangeAttackTarget(card, idx);
            var unit = BattleObjectManager.instance.GetAliveList(owner.faction).FirstOrDefault(x =>
                x.passiveDetail.HasPassive<PassiveAbility_MiyuNpc_Sa21341>());
            return unit ?? base.ChangeAttackTarget(card, idx);
        }
    }
}