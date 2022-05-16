using System.Linq;
using LOR_DiceSystem;
using VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_Ultima_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;
        private int _atkLand;

        public override void OnUseCard()
        {
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            var buff =
                owner.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_ShadowEntertainMe_Sa21341) as
                    BattleUnitBuf_ShadowEntertainMe_Sa21341;
            buff?.AddStacks(3);
        }
    }
}