using System.Linq;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.BluePetal.Passives;

namespace VortexLabyrinth_Sa21341.Aztec.Passives
{
    public class PassiveAbility_AztecShield_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 1, VortexModParameters.PackageId);
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            if (card.GetID() == new LorId(VortexModParameters.PackageId, 21)) return base.ChangeAttackTarget(card, idx);
            var unit = BattleObjectManager.instance.GetAliveList(owner.faction).FirstOrDefault(x =>
                x.passiveDetail.HasPassive<PassiveAbility_GuardianOfTheTower_Sa21341>());
            return unit ?? base.ChangeAttackTarget(card, idx);
        }
    }
}