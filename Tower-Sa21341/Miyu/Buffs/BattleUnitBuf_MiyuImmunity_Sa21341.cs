using VortexLabyrinth_Sa21341.Miyu.Passives;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_MiyuImmunity_Sa21341 : BattleUnitBuf
    {
        public override bool IsInvincibleHp(BattleUnitModel attacker)
        {
            if (attacker != null && attacker.passiveDetail.HasPassive<PassiveAbility_Healer_Sa21341>()) return true;
            return base.IsInvincibleHp(attacker);
        }

        public override bool IsInvincibleBp(BattleUnitModel attacker)
        {
            if (attacker != null && attacker.passiveDetail.HasPassive<PassiveAbility_Healer_Sa21341>()) return true;
            return base.IsInvincibleBp(attacker);
        }
    }
}