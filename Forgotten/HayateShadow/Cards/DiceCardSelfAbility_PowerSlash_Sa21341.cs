using LOR_DiceSystem;
using VortexTower.Forgotten.HayateShadow.Buffs;

namespace VortexTower.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_PowerSlash_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            var buf =
                owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_ShadowEntertainMe_Sa21341) as
                    BattleUnitBuf_ShadowEntertainMe_Sa21341;
            buf?.AddStacks(3);
        }
    }
}