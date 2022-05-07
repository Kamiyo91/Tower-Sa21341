using LOR_DiceSystem;
using VortexLabyrinth_Sa21341.Sae.Buffs;

namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_CombatReady_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>()) return;
            foreach (var battleDice in card.GetDiceBehaviorList())
            {
                battleDice.behaviourInCard = battleDice.behaviourInCard.Copy();
                battleDice.behaviourInCard.Detail = BehaviourDetail.Guard;
                battleDice.behaviourInCard.MotionDetail = MotionDetail.G;
                battleDice.behaviourInCard.Type = BehaviourType.Def;
                battleDice.behaviourInCard.EffectRes = "Hana_G";
            }
        }
    }
}