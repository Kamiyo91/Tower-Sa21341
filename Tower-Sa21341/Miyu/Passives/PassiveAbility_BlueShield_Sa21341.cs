using KamiyoStaticBLL.Models;
using Sound;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_BlueShield_Sa21341 : PassiveAbilityBase
    {
        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            if (ModParameters.OnlyAllyTargetCardIds.Contains(behavior.card.card.GetID()))
                return base.GetDamageReduction(behavior);
            owner.passiveDetail.DestroyPassive(this);
            if (owner.battleCardResultLog == null) return 9999;
            SingletonBehavior<DiceEffectManager>.Instance.CreateBehaviourEffect("BlueShield_Sa21341", 1f,
                owner.view, owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
            return 9999;
        }

        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            return ModParameters.OnlyAllyTargetCardIds.Contains(behavior.card.card.GetID())
                ? base.GetDamageReduction(behavior)
                : 9999;
        }

        public override void OnRoundEnd()
        {
            owner.passiveDetail.DestroyPassive(this);
        }
    }
}