using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Cards
{
    public class DiceCardSelfAbility_FieryBlueFire_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPoint(1);
            if (!(owner.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) is BattleUnitBuf_BlueFlame_Sa21341
                    buff) || buff.stack < 5) return;
            buff.AddStacks(-5);
            owner.allyCardDetail.DrawCards(1);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = 1
            });
        }
    }
}