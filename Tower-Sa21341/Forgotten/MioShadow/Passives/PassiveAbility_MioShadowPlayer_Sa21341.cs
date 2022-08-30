using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.CommonBuffs;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.MioShadow.Buffs;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension.MioShadow;

namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Passives
{
    public class PassiveAbility_MioShadowPlayer_Sa21341 : PassiveAbilityBase
    {
        private MechUtil_MioShadow _util;
        public override bool isImmortal => true;
        public override bool isInvincibleBp => true;
        private bool _staggered;
        public override void OnWaveStart()
        {
            owner.ignoreBloodyEffect = true;
            _util = new MechUtil_MioShadow(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_GodAuraRelease_Sa21341),
                RefreshUI = true
            });
            UnitUtil.CheckSkinProjection(owner);
            _util.ForcedEgo();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnRoundStartAfter()
        {
            if (!_staggered) return;
            _staggered = false;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_KamiyoLockedUnit());
        }
        public override int ChangeTargetSlot(BattleDiceCardModel card, BattleUnitModel target, int currentSlot,
            int targetSlot, bool teamkill)
        {
            return MechUtil_MioShadow.AlwaysAimToTheSlowestDice(target);
        }
        public override void AfterTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (owner.hp < 2)
                owner.breakDetail.LoseBreakLife(attacker);
        }

        public override void OnReleaseBreak()
        {
            owner.RecoverHP(owner.MaxHp);
        }
        public override void OnBreakState()
        {
            _staggered = true;
        }
        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            if(unit.faction == owner.faction && unit.Book.BookId == new LorId(VortexModParameters.PackageId,10000012)) owner.Die();
        }
    }
}
