using BigDLL4221.Extensions;
using LOR_DiceSystem;
using VortexTower.Sae.Buffs;
using VortexTower.Sae.Passives;

namespace VortexTower.Sae.Dice
{
    public class DiceCardAbility_CombatReadyCounterDice_Sa21341 : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            behavior.card.target?.GetActivePassive<PassiveAbility_SaeNpc_Sa21341>()?.SetImmortal(false);
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>()) return;
            behavior.behaviourInCard = behavior.behaviourInCard.Copy();
            behavior.behaviourInCard.Detail = BehaviourDetail.Guard;
            behavior.behaviourInCard.MotionDetail = MotionDetail.G;
            behavior.behaviourInCard.Type = BehaviourType.Standby;
            behavior.behaviourInCard.EffectRes = "Shi_G";
        }
    }
}