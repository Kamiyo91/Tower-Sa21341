using LOR_DiceSystem;
using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_CombatReady_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>() &&
                !owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralDefStance_Sa21341>()) return;
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