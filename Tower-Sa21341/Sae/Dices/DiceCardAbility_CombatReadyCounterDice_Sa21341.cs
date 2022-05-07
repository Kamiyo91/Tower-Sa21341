using LOR_DiceSystem;
using VortexLabyrinth_Sa21341.Sae.Buffs;

namespace VortexLabyrinth_Sa21341.Sae.Dices
{
    public class DiceCardAbility_CombatReadyCounterDice_Sa21341 : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>()) return;
            behavior.behaviourInCard = behavior.behaviourInCard.Copy();
            behavior.behaviourInCard.Detail = BehaviourDetail.Guard;
            behavior.behaviourInCard.MotionDetail = MotionDetail.G;
            behavior.behaviourInCard.Type = BehaviourType.Standby;
            behavior.behaviourInCard.EffectRes = "Shi_G";
        }
    }
}